using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.core;
using modules.state_machine.state;
using UnityEngine;
using Zenject;

public class Prefab : MonoBehaviour, IDisposable {
    private IStateTriggerDispatcher _state_trigger_dispatcher;

    [Inject]
    public void Constructor(IStateTriggerDispatcher state_trigger_dispatcher) {
        _state_trigger_dispatcher = state_trigger_dispatcher;
        _state_trigger_dispatcher.StateTrigger<PrefabTrigger>();
    }

    private void OnDestroy() {
        Dispose();
    }

    public void Dispose() {
        _state_trigger_dispatcher?.Dispose();
    }

    public class Factory : PlaceholderFactory<Prefab>{}

    public class PrefabAState : TransitionState<PrefabTrigger> {
        public override async UniTask Thru(CancellationToken cancellation_token) {
            Debug.Log($" -- Prefab a state -- ");
            await UniTask.Delay(TimeSpan.FromSeconds(0.5), cancellationToken: cancellation_token);
        }
    }

    public class PrefabBState : BaseGameState<PrefabTrigger> {
        
        private readonly IStateTriggerDispatcher _state_trigger_dispatcher;

        public PrefabBState(IStateTriggerDispatcher state_trigger_dispatcher) {
            _state_trigger_dispatcher = state_trigger_dispatcher;
        }
        
        public override void Enter() {
            Debug.Log($" -- Prefab b state -- ");
            _state_trigger_dispatcher.StateTrigger<PrefabTrigger>();
        }
    }
}