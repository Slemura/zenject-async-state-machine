using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.AsyncStateMachine.State;

namespace RpDev.AsyncStateMachine.Sample.States
{
    public class LoadLevelState : TransitionState<SampleTriggers.StartLevelTrigger>
    {
        private readonly CallCounter _counter;

        public LoadLevelState(CallCounter counter)
        {
            _counter = counter;
        }

        public override async UniTask Thru(CancellationToken cancellationToken)
        {
            Logger.Log($"{_counter.Count} ===LoadLevelState {TriggerInfo.LevelNum}===");
            _counter.Increment();
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, PlayerLoopTiming.Initialization,
                cancellationToken);
        }
    }
}