using modules.state_machine.trigger;

namespace modules.state_machine.Sample {
    public class SampleTriggers {
    
        public class MainMenuTrigger : BaseStateTrigger {}

        public class StartGameTrigger : MainMenuTrigger {}

        public class StartLevelTrigger : BaseStateTrigger {
            public int level_num;
        }
    }
}
