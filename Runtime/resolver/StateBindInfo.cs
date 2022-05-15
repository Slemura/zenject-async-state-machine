using System;
using System.Collections.Generic;
using modules.state_machine.state;

namespace modules.state_machine.resolver {
    
    public struct StateBindInfo  {
        
        public Type trigger_type;

        public IReadOnlyList<StateCreationType>                                 thru_state_creations;
        public StateCreationType                                                state_creation_type;
        public Func<object, StateCreationType, IBaseState>                      state_enter_resolve_func;
        public IReadOnlyList<Func<object, StateCreationType, ITransitionState>> thru_resolve_func;
        
    }

    public enum StateCreationType {
        AsNew,
        AsCached
    }
}