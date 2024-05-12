using RpDev.AsyncStateMachine.Trigger;

namespace RpDev.AsyncStateMachine.Sample
{
    public class SampleTriggers
    {
        public class MainMenuTrigger : BaseStateTrigger
        {
        }

        public class StartGameTrigger : MainMenuTrigger
        {
        }

        public class StartLevelTrigger : BaseStateTrigger
        {
            public int LevelNum;
        }
    }
}