using ModestTree;
using modules.state_machine.core;
using Zenject;

namespace modules.state_machine.resolver {
    public class AsyncStateMachineInstaller : Installer<AsyncStateMachineInstaller> {
        public override void InstallBindings() {
            
            Container.Settings = new ZenjectSettings(validationErrorResponse: ValidationErrorResponses.Log, RootResolveMethods.All, displayWarningWhenResolvingDuringInstall: false);
            
            Assert.That(Container.HasBinding<AsyncStateMachine>() == false, "Detected multiple ContextStateMachine bindings.  StateMachineInstaller should only be installed once");
            
            Container.BindInterfacesTo<StateTriggerDispatcher>().AsSingle().CopyIntoAllSubContainers();
            Container.BindInterfacesTo<AsyncStateMachine>().AsSingle().CopyIntoAllSubContainers();
            Container.BindInterfacesTo<StateFactory>().AsSingle().CopyIntoAllSubContainers();
        }
    }
}