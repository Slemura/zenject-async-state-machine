using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ModestTree;
using RpDev.AsyncStateMachine.Resolver;
using RpDev.AsyncStateMachine.State;
using Zenject;

namespace RpDev.AsyncStateMachine.Core
{
    public class AsyncStateMachine : IAsyncStateMachine
    {
        private readonly IAsyncStateMachine _parentStateMachine;
        private readonly Dictionary<Type, StateBindInfo> _statesInfo = new Dictionary<Type, StateBindInfo>();
        private readonly string _guid;

        private IBaseState _currentState;
        private ITransitionState _currentTransitionState;
        private IAbstractState _prevState;

        private CancellationTokenSource _cancellationToken;
        private bool _isDisposed;

        public AsyncStateMachine(
            [Inject(Source = InjectSources.Parent, Optional = true)]
            IAsyncStateMachine parentStateMachine)
        {
            _guid = Guid.NewGuid().ToString();
            _parentStateMachine = parentStateMachine;
            _cancellationToken = new CancellationTokenSource();
        }

        public void AddStateInfo(StateBindInfo info)
        {
            _statesInfo.Add(info.TriggerType, info);
        }

        public void EnterStateBeTriggerType(object trigger)
        {
            var triggerType = trigger.GetType();

            if (_statesInfo.ContainsKey(triggerType) == false && _parentStateMachine != null)
            {
                _parentStateMachine.EnterStateBeTriggerType(trigger);
                return;
            }

            Assert.That(_statesInfo.ContainsKey(triggerType),
                $"Trigger {triggerType.Name} type not registered in State machine {_guid}");

            EnterStateByInfo(_statesInfo[triggerType], trigger).Forget();
        }

        private async UniTask EnterStateByInfo(StateBindInfo info, object trigger)
        {
            if (_isDisposed) return;

            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();

            _prevState = _currentState;

            if (_currentState != null)
            {
                await _currentState.Exit(_cancellationToken.Token);
            }

            if (info.ThruResolveFunc != null && info.ThruResolveFunc.Count > 0)
            {
                for (var i = 0; i < info.ThruResolveFunc.Count; i++)
                {
                    if (_isDisposed) break;

                    _currentTransitionState = info.ThruResolveFunc[i]
                        .Invoke(trigger, _prevState?.GetType(), info.ThruStateCreations[i]);

                    _prevState = _currentTransitionState;

                    await _currentTransitionState.Thru(_cancellationToken.Token);
                }
            }

            if (_isDisposed) return;

            _currentState = info.StateEnterResolveFunc.Invoke(trigger, _prevState?.GetType(), info.StateCreationType);
            _currentState.Enter();

            _currentTransitionState = null;
            _cancellationToken.Dispose();
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;
            _statesInfo.Clear();
            _cancellationToken?.Dispose();
        }
    }
}