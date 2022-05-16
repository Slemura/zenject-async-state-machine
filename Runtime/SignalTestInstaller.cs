
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.core;
using modules.state_machine.resolver;
using modules.state_machine.state;
using modules.state_machine.trigger;
using UnityEngine;
using Zenject;

public class SignalTestInstaller : MonoInstaller {
    
    public override void InstallBindings() {
        AsyncStateMachineInstaller.Install(Container);

        Container.BindStateTrigger<StartGameTrigger>()
                 .ThruState<InitLibsTransitionState>()
                 .ToState<StartGameState>();
        
        Container.BindStateTrigger<GameOverTrigger>()
                 .ToState<GameOverState>();
        
        Container.BindInterfacesTo<Launcher>().AsSingle().NonLazy();
        
        Container.BindFactory<Prefab, Prefab.Factory>()
                 .FromSubContainerResolve()
                 .ByMethod(InstallPrefab).AsSingle();
    }

    private void InstallPrefab(DiContainer container) {
        
        container.BindStateTrigger<PrefabTrigger>()
                 .ThruState<Prefab.PrefabAState>()
                 .ToState<Prefab.PrefabBState>();
        
        container.BindInterfacesAndSelfTo<Prefab>().FromNewComponentOnNewGameObject().WithGameObjectName("Prefab").AsSingle();
    }

    public class Launcher : IInitializable {
        private readonly IStateTriggerDispatcher _state_trigger_dispatcher;

        private readonly Prefab.Factory _prefab_factory;

        public Launcher(IStateTriggerDispatcher state_trigger_dispatcher) {
            _state_trigger_dispatcher = state_trigger_dispatcher;
        }
        
        public void Initialize() {
            _state_trigger_dispatcher.StateTrigger<StartGameTrigger>();    
        }
    }
}

public class StartGameTrigger : BaseStateTrigger {}
public class GameOverTrigger : BaseStateTrigger {
    public Prefab prefab;
}

public class PrefabTrigger : BaseStateTrigger {}



public class InitLibsTransitionState : TransitionState<StartGameTrigger> {
    public override async UniTask Thru(CancellationToken cancellation_token) {
        Debug.LogError($" -- StartGame State -- ");
        await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: cancellation_token);
    }
}

public class StartGameState : BaseGameState<StartGameTrigger> {
    
    private readonly Prefab.Factory          _prefab_factory;
    private readonly IStateTriggerDispatcher _state_trigger_dispatcher;

    public StartGameState(Prefab.Factory prefab_factory, IStateTriggerDispatcher state_trigger_dispatcher) {
        
        _prefab_factory           = prefab_factory;
        _state_trigger_dispatcher = state_trigger_dispatcher;
    }
    
    public override async void Enter() {
        Prefab prefab = _prefab_factory.Create();
        Debug.LogError($" -- Start Game State --");
        await UniTask.Delay(TimeSpan.FromSeconds(5));
        
        Debug.LogError($" -- Dispatch Game Over Trigger --");
        _state_trigger_dispatcher.StateTrigger(new GameOverTrigger {
            prefab = prefab
        });
    }
}

public class GameOverState : BaseGameState<GameOverTrigger> {
    
    public override void Enter() {
        Debug.LogError($" -- Game over state -- ");
        GameObject.Destroy(trigger_info.prefab.gameObject);
    }
}