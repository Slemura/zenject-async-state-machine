using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.sample;
using modules.state_machine.state;

namespace modules.state_machine.Sample.states {
    public class InitGameState : TransitionState<SampleTriggers.StartGameTrigger> {
        
        private readonly CallCounter _counter;

        public InitGameState(CallCounter counter) {
            _counter = counter;
        }
        
        public override async UniTask Thru(CancellationToken cancellation_token) {
            Cl.Log($"{_counter.Count} ===InitGameState===");
            _counter.Increment();
            await UniTask.Delay(TimeSpan.FromSeconds(2), DelayType.Realtime, PlayerLoopTiming.Initialization, cancellation_token);
        }
    }
}