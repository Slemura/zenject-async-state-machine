using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.core;
using modules.state_machine.sample;
using modules.state_machine.state;
using UnityEngine;

namespace modules.state_machine.Sample.states {
    public class MainMenuState : BaseGameState<SampleTriggers.MainMenuTrigger> {
        
        private readonly CallCounter             _counter;
        private readonly IStateTriggerDispatcher _state_dispatcher;

        public MainMenuState(CallCounter counter, IStateTriggerDispatcher state_dispatcher) {
            _counter          = counter;
            _state_dispatcher = state_dispatcher;
        }
        
        public override void Enter() {
            Cl.Log($"{_counter.Count} ===MainMenuState=== {PrevStateType?.Name}");
            _counter.Increment();
            _state_dispatcher.StateTrigger(new SampleTriggers.StartLevelTrigger {
                level_num = 5
            });
        }

        public override UniTask Exit(CancellationToken cancellation_token) {
            Cl.Log($"{_counter.Count}---Exit from MainMenuState", Color.red);
            _counter.Increment();
            return base.Exit(cancellation_token);
        }
    }
}