using RpDev.AsyncStateMachine.Core;
using Zenject;

namespace RpDev.AsyncStateMachine.Sample
{
    public class StubLauncher : IInitializable
    {
        private readonly IStateTriggerDispatcher _stateDispatcher;

        public StubLauncher(IStateTriggerDispatcher stateDispatcher)
        {
            _stateDispatcher = stateDispatcher;
        }

        public void Initialize()
        {
            _stateDispatcher.StateTrigger<SampleTriggers.StartGameTrigger>();
        }
    }
}