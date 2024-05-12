using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.AsyncStateMachine.Core;
using RpDev.AsyncStateMachine.State;

namespace RpDev.AsyncStateMachine.Sample.States
{
    public class InitLibsState : TransitionState<SampleTriggers.StartGameTrigger>
    {
        private readonly CallCounter _counter;
        private readonly IStateTriggerDispatcher _stateTriggerDispatcher;

        public InitLibsState(CallCounter counter, IStateTriggerDispatcher stateTriggerDispatcher)
        {
            _counter = counter;
            _stateTriggerDispatcher = stateTriggerDispatcher;
        }

        public override async UniTask Thru(CancellationToken cancellationToken)
        {
            Logger.Log($"{_counter.Count} ===InitLibsState=== prev state {PrevStateType?.Name}");
            _counter.Increment();
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, PlayerLoopTiming.Initialization,
                cancellationToken);
        }
    }
}