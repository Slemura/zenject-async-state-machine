using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ModestTree;
using modules.state_machine.resolver;
using modules.state_machine.state;
using Zenject;

namespace modules.state_machine.core {
    
    public interface IAsyncStateMachine : IDisposable {
        void EnterStateBeTriggerType(object trigger);
        void AddStateInfo(StateBindInfo     info);
    }

    public class AsyncStateMachine : IAsyncStateMachine {

        private readonly IAsyncStateMachine              _parent_state_machine;
        private readonly Dictionary<Type, StateBindInfo> _states_info = new Dictionary<Type, StateBindInfo>();
        private readonly string                          _guid;

        private IBaseState       _current_state;
        private ITransitionState _current_transition_state;
        private IAbstractState   _prev_state;

        private          CancellationTokenSource _cancellation_token;
        private          bool                    _is_disposed;
        
        public AsyncStateMachine([Inject(Source = InjectSources.Parent, Optional = true)]
                                   IAsyncStateMachine parent_state_machine) {

            _guid                 = Guid.NewGuid().ToString();
            _parent_state_machine = parent_state_machine;
            _cancellation_token   = new CancellationTokenSource();
        }
        
        public void AddStateInfo(StateBindInfo info) {
            _states_info.Add(info.trigger_type, info);
        }

        public void EnterStateBeTriggerType(object trigger) {
            
            Type trigger_type = trigger.GetType();
            
            if (_states_info.ContainsKey(trigger_type) == false && _parent_state_machine != null) {
                _parent_state_machine.EnterStateBeTriggerType(trigger);
                return;
            }

            Assert.That(_states_info.ContainsKey(trigger_type), $"Trigger {trigger_type.Name} type not registered in State machine {_guid}");
            
            EnterStateByInfo(_states_info[trigger_type], trigger).Forget();
        }

        private async UniTask EnterStateByInfo(StateBindInfo info, object trigger) {
            if(_is_disposed) return;
            
            _cancellation_token?.Dispose();
            _cancellation_token = new CancellationTokenSource();
            
            _prev_state         = _current_state;
            
            if (_current_state != null) {
                await _current_state.Exit(_cancellation_token.Token);
            }

            if (info.thru_resolve_func != null && info.thru_resolve_func.Count > 0) {
                for (int i = 0; i < info.thru_resolve_func.Count; i++) {
                    if(_is_disposed) break;
                    
                    _current_transition_state = info.thru_resolve_func[i].Invoke(trigger, _prev_state?.GetType(), info.thru_state_creations[i]);
                    
                    _prev_state = _current_transition_state;
                    
                    await _current_transition_state.Thru(_cancellation_token.Token);
                }
            }
            
            if(_is_disposed) return;
            
            _current_state = info.state_enter_resolve_func.Invoke(trigger, _prev_state?.GetType(), info.state_creation_type);
            _current_state.Enter();
            
            _current_transition_state = null;
            _cancellation_token.Dispose();
        }

        public void Dispose() {
            if(_is_disposed) return;
            _is_disposed = true;
            _states_info.Clear();
            _cancellation_token?.Dispose();
        }
    }
}