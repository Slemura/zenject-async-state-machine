using modules.state_machine.resolver;
using modules.state_machine.Sample;
using modules.state_machine.Sample.states;
using Zenject;

namespace modules.state_machine.sample {
    
    public class SampleStateInstaller : MonoInstaller {
    
        public override void InstallBindings() {
            
            StateMachineInstaller.Install(Container);
            
            Container.Bind<CallCounter>().AsSingle();


            Container.SetStateTransition<SampleTriggers.StartGameTrigger>()
                     .ThruState<InitGameState>()
                     .ThruState<InitLibsState>()
                     .ToState<MainMenuState>(StateCreationType.AsCached);

            Container.SetStateTransition<SampleTriggers.MainMenuTrigger>()
                     .ToState<MainMenuState>();
            
            Container.SetStateTransition<SampleTriggers.StartLevelTrigger>()
                     .ThruState<LoadLevelState>()
                     .ToState<GameplayState>();

            Container.BindInterfacesTo<StubLauncher>().AsSingle().NonLazy();
        }
    }
}
