using System;
using ModestTree;
using modules.state_machine.resolver;

namespace modules.state_machine.core {
    public interface IStateTriggerDispatcher {
        void StateTrigger<TBaseStateTrigger>();
        void StateTrigger(object trigger_instance);
    }

    public class StateTriggerDispatcher : IStateTriggerDispatcher {
        
        private readonly IContextStateMapper  _context_state_mapper;
        private readonly IContextStateMachine _context_state_machine;

        public StateTriggerDispatcher(IContextStateMapper context_state_mapper, IContextStateMachine context_state_machine) {
            _context_state_mapper  = context_state_mapper;
            _context_state_machine = context_state_machine;
        }
        
        public void StateTrigger<TBaseStateTrigger>() {
            StateTrigger(Activator.CreateInstance<TBaseStateTrigger>());
        }

        public void StateTrigger(object trigger_instance) {
            StateBindInfo info = _context_state_mapper.GetBindInfoByTransitionType(trigger_instance.GetType());
            
            Assert.That(info.trigger_type != null);
            
            _context_state_machine.EnterStateByInfo(info, trigger_instance);
        }
    }
}
