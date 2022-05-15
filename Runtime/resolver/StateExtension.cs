using modules.state_machine.core;
using modules.state_machine.trigger;
using Zenject;

namespace modules.state_machine.resolver {
    
    public static class StateExtension {

        private static IContextStateMapper _context_state_mapper;
        private static IStateFactory       _state_factory;

        public static StateBinder<TTransition> SetStateTransition<TTransition>(this DiContainer container) where TTransition : BaseStateTrigger {
            return container.CreateSubContainer().Instantiate<StateBinder<TTransition>>();
        }
    }

    public class StateMachineInstaller : Installer<StateMachineInstaller> {
        public override void InstallBindings() {
            Container.BindInterfacesTo<StateTriggerDispatcher>().AsSingle();
            Container.BindInterfacesTo<ContextStateMapper>().AsSingle();
            Container.BindInterfacesTo<ContextStateMachine>().AsSingle();
            Container.BindInterfacesTo<StateFactory>().AsSingle();
        }
    }
}
