using modules.state_machine.resolver;
using modules.state_machine.Sample;
using modules.state_machine.Sample.states;
using Zenject;

namespace modules.state_machine.sample {
    
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
