using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.sample;
using modules.state_machine.state;
using UnityEngine;

namespace modules.state_machine.Sample.states {
    public class LoadLevelState : TransitionState<SampleTriggers.StartLevelTrigger> {
        private readonly CallCounter _counter;

        public LoadLevelState(CallCounter counter) {
            _counter = counter;
        }
        
        public override async UniTask Thru(CancellationToken cancellation_token) {
            Cl.Log($"{_counter.Count} ===LoadLevelState {trigger_info.level_num}===");
            _counter.Increment();
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, PlayerLoopTiming.Initialization, cancellation_token);
        }
    }
}