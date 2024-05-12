using RpDev.AsyncStateMachine.State;

namespace RpDev.AsyncStateMachine.Sample.States
{
    public class GameplayState : BaseGameState<SampleTriggers.StartLevelTrigger>
    {
        private readonly CallCounter _counter;

        public GameplayState(CallCounter counter)
        {
            _counter = counter;
        }

        public override void Enter()
        {
            Logger.Log($"{_counter.Count} ===GameplayState {TriggerInfo.LevelNum} prev state {PrevStateType?.Name}===");
            _counter.Increment();
        }
    }
}