using System;
using System.Collections.Generic;
using modules.state_machine.state;

namespace modules.state_machine.resolver {
    
    public struct StateBindInfo  {
        
        public Type trigger_type;

        public IReadOnlyList<StateCreationType>                                 thru_state_creations;
        public StateCreationType                                                state_creation_type;
        public Func<object, Type, StateCreationType, IBaseState>                      state_enter_resolve_func;
        public IReadOnlyList<Func<object, Type, StateCreationType, ITransitionState>> thru_resolve_func;
        
    }

    public enum StateCreationType {
        AsNew,
        AsCached
    }
}