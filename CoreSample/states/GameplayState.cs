using modules.state_machine.sample;
using modules.state_machine.state;

namespace modules.state_machine.Sample.states {
    public class GameplayState : BaseGameState<SampleTriggers.StartLevelTrigger> {
        
        private readonly CallCounter _counter;

        public GameplayState(CallCounter counter) {
            _counter = counter;
        }
        
        public override void Enter() {
            Cl.Log($"{_counter.Count} ===GameplayState {TriggerInfo.level_num} prev state {PrevStateType?.Name}===");
            _counter.Increment();
        }
    }
}