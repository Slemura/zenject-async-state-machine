using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.core;
using modules.state_machine.sample;
using modules.state_machine.state;

namespace modules.state_machine.Sample.states {
    public class InitLibsState : TransitionState<SampleTriggers.StartGameTrigger> {
        
        private readonly CallCounter             _counter;
        private readonly IStateTriggerDispatcher _state_trigger_dispatcher;

        public InitLibsState(CallCounter counter, IStateTriggerDispatcher state_trigger_dispatcher) {
            _counter                  = counter;
            _state_trigger_dispatcher = state_trigger_dispatcher;
        }

        public override async UniTask Thru(CancellationToken cancellation_token) {
            Cl.Log($"{_counter.Count} ===InitLibsState===");
            _counter.Increment();
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, PlayerLoopTiming.Initialization, cancellation_token);
        }
    }
}