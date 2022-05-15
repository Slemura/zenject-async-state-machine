using modules.state_machine.core;
using modules.state_machine.Sample;
using Zenject;

namespace modules.state_machine.sample {
    public class StubLauncher : IInitializable {
        
        private readonly IStateTriggerDispatcher _state_dispatcher;

        public StubLauncher(IStateTriggerDispatcher state_dispatcher) {
            _state_dispatcher = state_dispatcher;
        }

        public void Initialize() {
            _state_dispatcher.StateTrigger<SampleTriggers.StartGameTrigger>();
        }
    }
}