using System;
using System.Collections.Generic;
using modules.state_machine.resolver;
using modules.state_machine.state;
using modules.state_machine.trigger;
using Zenject;

namespace modules.state_machine.core {
    public interface IStateFactory {
        IState<TTrigger> Create<TTrigger>(Type state_type, StateCreationType state_creation_type) where TTrigger : BaseStateTrigger;
        ITransitionState<TTrigger> CreateTransition<TTrigger>(Type state_type, StateCreationType state_creation_type) where TTrigger : BaseStateTrigger;
    }

    public class StateFactory : IStateFactory {
        
        private readonly IInstantiator                    _di_container;
        private readonly Dictionary<Type, IAbstractState> _cached_states = new Dictionary<Type, IAbstractState>();

        
        public StateFactory(IInstantiator di_container) {
            _di_container = di_container;
        }

        public IState<TTrigger> Create<TTrigger>(Type state_type, StateCreationType state_creation_type) where TTrigger : BaseStateTrigger {
            
            switch (state_creation_type) {
                case StateCreationType.AsNew: return (IState<TTrigger>)_di_container.Instantiate(state_type);        
                    
                case StateCreationType.AsCached:
                    if (_cached_states.ContainsKey(state_type) == false) {
                        _cached_states.Add(state_type, (IBaseState)_di_container.Instantiate(state_type));
                    }

                    return (IState<TTrigger>) _cached_states[state_type];
                default: return (IState<TTrigger>)_di_container.Instantiate(state_type);
            }
        }
        
        public ITransitionState<TTrigger> CreateTransition<TTrigger>(Type state_type, StateCreationType state_creation_type) where TTrigger : BaseStateTrigger {
            
            switch (state_creation_type) {
                case StateCreationType.AsNew: return (ITransitionState<TTrigger>)_di_container.Instantiate(state_type);        
                    
                case StateCreationType.AsCached:
                    if (_cached_states.ContainsKey(state_type) == false) {
                        _cached_states.Add(state_type, (IBaseState)_di_container.Instantiate(state_type));
                    }

                    return (ITransitionState<TTrigger>) _cached_states[state_type];
                default: return (ITransitionState<TTrigger>)_di_container.Instantiate(state_type);
            }
        }
    }
}
