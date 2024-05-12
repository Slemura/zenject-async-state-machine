using RpDev.AsyncStateMachine.Installer;
using RpDev.AsyncStateMachine.Resolver;
using RpDev.AsyncStateMachine.Sample.States;
using Zenject;

namespace RpDev.AsyncStateMachine.Sample {
    
    public class SampleStateInstaller : MonoInstaller {
    
        public override void InstallBindings() {
            
            AsyncStateMachineInstaller.Install(Container);
            
            Container.Bind<CallCounter>().AsSingle();


            Container.BindStateTrigger<SampleTriggers.StartGameTrigger>()
                     .ThruState<InitGameState>()
                     .ThruState<InitLibsState>()
                     .ToState<MainMenuState>(StateCreationType.AsCached);

            Container.BindStateTrigger<SampleTriggers.MainMenuTrigger>()
                     .ToState<MainMenuState>();
            
            Container.BindStateTrigger<SampleTriggers.StartLevelTrigger>()
                     .ThruState<LoadLevelState>()
                     .ToState<GameplayState>();

            Container.BindInterfacesTo<StubLauncher>().AsSingle().NonLazy();
        }
    }
}
