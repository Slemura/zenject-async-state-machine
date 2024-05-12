using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.AsyncStateMachine.State;

namespace RpDev.AsyncStateMachine.Sample.States
{
    public class InitGameState : TransitionState<SampleTriggers.StartGameTrigger>
    {
        private readonly CallCounter _counter;

        public InitGameState(CallCounter counter)
        {
            _counter = counter;
        }

        public override async UniTask Thru(CancellationToken cancellationToken)
        {
            Logger.Log($"{_counter.Count} ===InitGameState===");
            _counter.Increment();
            await UniTask.Delay(TimeSpan.FromSeconds(2), DelayType.Realtime, PlayerLoopTiming.Initialization, cancellationToken);
        }
    }
}