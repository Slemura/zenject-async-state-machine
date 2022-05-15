using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.resolver;
using modules.state_machine.state;

namespace modules.state_machine.core {
    
    public interface IContextStateMachine {
        UniTask EnterStateByInfo(StateBindInfo info, object transition_object);
    }

    public class ContextStateMachine : IContextStateMachine {
        
        private readonly IStateFactory _state_factory;

        private IBaseState       _current_state;
        private ITransitionState _current_transition_state;
        private IBaseState       _prev_state;
        
        private CancellationTokenSource _cancellation_token;

        public ContextStateMachine(IStateFactory state_factory) {
            _state_factory      = state_factory;
            _cancellation_token = new CancellationTokenSource();
        }
        
        public async UniTask EnterStateByInfo(StateBindInfo info, object transition_object) {
            if (_current_state != null) {
                await _current_state.Exit(_cancellation_token.Token);
            }

            if (info.thru_resolve_func != null && info.thru_resolve_func.Count > 0) {
                for (int i = 0; i < info.thru_resolve_func.Count; i++) {
                    _current_transition_state = info.thru_resolve_func[i].Invoke(transition_object, info.thru_state_creations[i]);
                    await _current_transition_state.Thru(_cancellation_token.Token);
                }
            }
            
            _current_state = info.state_enter_resolve_func.Invoke(transition_object, info.state_creation_type);
            _current_state.Enter();
            
            _cancellation_token.Dispose();
        }
    }
}