using System;
using Zenject;

namespace RpDev.AsyncStateMachine.Core
{
    public class StateTriggerDispatcher : IStateTriggerDispatcher
    {
        private readonly IAsyncStateMachine _asyncStateMachine;

        private bool _isDisposed = false;

        public StateTriggerDispatcher([Inject(Source = InjectSources.Local)] IAsyncStateMachine asyncStateMachine)
        {
            _asyncStateMachine = asyncStateMachine;
        }

        public void StateTrigger<TBaseStateTrigger>()
        {
            if (_isDisposed) return;
            StateTrigger(Activator.CreateInstance<TBaseStateTrigger>());
        }

        public void StateTrigger(object triggerInstance)
        {
            if (_isDisposed) return;
            _asyncStateMachine.EnterStateBeTriggerType(triggerInstance);
        }

        public void Dispose()
        {
            _isDisposed = true;
            _asyncStateMachine?.Dispose();
        }
    }
}