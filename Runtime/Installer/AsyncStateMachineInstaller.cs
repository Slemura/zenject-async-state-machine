using ModestTree;
using RpDev.AsyncStateMachine.Core;
using Zenject;

namespace RpDev.AsyncStateMachine.Installer
{
    public class AsyncStateMachineInstaller : Installer<AsyncStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Settings = new ZenjectSettings(validationErrorResponse: ValidationErrorResponses.Log,
                RootResolveMethods.All, displayWarningWhenResolvingDuringInstall: false);

            Assert.That(Container.HasBinding<Core.AsyncStateMachine>() == false,
                "Detected multiple ContextStateMachine bindings.  StateMachineInstaller should only be installed once");

            Container.BindInterfacesTo<StateTriggerDispatcher>().AsSingle().CopyIntoAllSubContainers();
            Container.BindInterfacesTo<Core.AsyncStateMachine>().AsSingle().CopyIntoAllSubContainers();
            Container.BindInterfacesTo<StateFactory>().AsSingle().CopyIntoAllSubContainers();
        }
    }
}