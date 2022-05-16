using modules.state_machine.trigger;
using Zenject;

namespace modules.state_machine.resolver {
    
    public static class StateExtension {
        public static StateBinder<TTransition> BindStateTrigger<TTransition>(this DiContainer container) where TTransition : BaseStateTrigger {
            return container.Instantiate<StateBinder<TTransition>>();
        }
    }
}
