using System;
using Zenject;

namespace modules.state_machine.core {
    
    public interface IStateTriggerDispatcher : IDisposable {
        void StateTrigger<TBaseStateTrigger>();
        void StateTrigger(object trigger_instance);
    }

    public class StateTriggerDispatcher : IStateTriggerDispatcher {

        private readonly IAsyncStateMachine _async_state_machine;

        private bool _is_disposed = false;
        
        public StateTriggerDispatcher([Inject(Source = InjectSources.Local)]
                                      IAsyncStateMachine async_state_machine) {
            
            _async_state_machine = async_state_machine;
        }
        
        public void StateTrigger<TBaseStateTrigger>() {
            if(_is_disposed) return;
            StateTrigger(Activator.CreateInstance<TBaseStateTrigger>());
        }

        public void StateTrigger(object trigger_instance) {
            if(_is_disposed) return;
            _async_state_machine.EnterStateBeTriggerType(trigger_instance);
        }

        public void Dispose() {
            _is_disposed = true;
            _async_state_machine?.Dispose();    
        }
    }
}
