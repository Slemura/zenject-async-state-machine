using System;
using System.Collections.Generic;

namespace modules.state_machine.resolver {
    public interface IContextStateMapper {
        void          AddStateInfo(StateBindInfo       info);
        StateBindInfo GetBindInfoByTransitionType(Type type);
    }

    public class ContextStateMapper : IContextStateMapper {
        
        private readonly Dictionary<Type, StateBindInfo> _states_info = new Dictionary<Type, StateBindInfo>();

        public void AddStateInfo(StateBindInfo info) {
            _states_info.Add(info.trigger_type, info);
        }

        public StateBindInfo GetBindInfoByTransitionType(Type type) {
            return _states_info.ContainsKey(type) ? _states_info[type] : default;
        }
    }
}
