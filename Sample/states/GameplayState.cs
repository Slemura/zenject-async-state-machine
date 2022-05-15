using modules.state_machine.sample;
using modules.state_machine.state;

namespace modules.state_machine.Sample.states {
    public class GameplayState : BaseGameState<SampleTriggers.StartLevelTrigger> {
        
        private readonly CallCounter _counter;

        public GameplayState(CallCounter counter) {
            _counter = counter;
        }
        
        public override void Enter() {
            Cl.Log($"{_counter.Count} ===GameplayState {trigger_info.level_num}===");
            _counter.Increment();
        }
    }
}