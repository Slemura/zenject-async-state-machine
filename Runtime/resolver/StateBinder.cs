using System;
using System.Collections.Generic;
using modules.state_machine.core;
using modules.state_machine.state;
using modules.state_machine.trigger;
using Zenject;

namespace modules.state_machine.resolver {
    
    public class StateBinder<TTrigger> : IDisposable where TTrigger : BaseStateTrigger {

        private readonly IAsyncStateMachine _state_machine;
        private readonly IStateFactory        _state_factory;
        
        private readonly List<Func<object, StateCreationType, ITransitionState>> _stack_of_thru_resolvers;
        private readonly List<StateCreationType>                                 _state_creations;
        
        private StateBindInfo _info;
        
        public StateBinder([Inject(Source = InjectSources.Local)]
                            IAsyncStateMachine state_machine, 
                            
                            [Inject(Source = InjectSources.Local)]
                            IStateFactory state_factory) {

            _state_machine = state_machine;
            _state_factory = state_factory;
            
            _info                    = new StateBindInfo();
            _stack_of_thru_resolvers = new List<Func<object, StateCreationType, ITransitionState>>();
            _state_creations         = new List<StateCreationType>();
        }
        
        public StateBinder<TTrigger> ThruState<TState>(StateCreationType creation_type = StateCreationType.AsNew) where TState : ITransitionState<TTrigger> {
            _state_creations.Add(creation_type);
            _stack_of_thru_resolvers.Add(TransitionStateGetterResolver<TState>);

            return this;
        }

        public void ToState<TState>(StateCreationType creation_type = StateCreationType.AsNew) where TState : IState<TTrigger> {
            
            _info.trigger_type             = typeof(TTrigger);
            _info.thru_resolve_func        = _stack_of_thru_resolvers;
            _info.thru_state_creations     = _state_creations;
            _info.state_creation_type      = creation_type;
            _info.state_enter_resolve_func = StateGetterResolver<TState>;
            
            _state_machine.AddStateInfo(_info);
        }

        private ITransitionState TransitionStateGetterResolver<TState>(object trigger, StateCreationType state_creation_type_type) where TState : ITransitionState<TTrigger>  {
            ITransitionState<TTrigger> state = _state_factory.CreateTransition<TTrigger>(typeof(TState), state_creation_type_type);
            state.SetupTriggerInfo((TTrigger)trigger);
            return state;
        }
        
        private IState<TTrigger> StateGetterResolver<TState>(object trigger, StateCreationType state_creation_type_type) where TState : IState<TTrigger> {
            IState<TTrigger> state = _state_factory.Create<TTrigger>(typeof(TState), state_creation_type_type);
            state.SetupTriggerInfo((TTrigger)trigger);
            return state;
        }

        public void Dispose() {
            _stack_of_thru_resolvers?.Clear();
            _state_creations?.Clear();
            _info = default;
        }
    }
}