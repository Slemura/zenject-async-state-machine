using System;
using System.Collections.Generic;
using RpDev.AsyncStateMachine.Core;
using RpDev.AsyncStateMachine.State;
using RpDev.AsyncStateMachine.Trigger;
using Zenject;

namespace RpDev.AsyncStateMachine.Resolver
{
    public class StateBinder<TTrigger> : IDisposable where TTrigger : BaseStateTrigger
    {
        private readonly IAsyncStateMachine _stateMachine;
        private readonly IStateFactory _stateFactory;

        private readonly List<Func<object, Type, StateCreationType, ITransitionState>> _stackOfThruResolvers;
        private readonly List<StateCreationType> _stateCreations;

        private StateBindInfo _info;

        public StateBinder([Inject(Source = InjectSources.Local)] IAsyncStateMachine stateMachine,
            [Inject(Source = InjectSources.Local)] IStateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;

            _info = new StateBindInfo();
            _stackOfThruResolvers = new List<Func<object, Type, StateCreationType, ITransitionState>>();
            _stateCreations = new List<StateCreationType>();
        }

        public StateBinder<TTrigger> ThruState<TState>(StateCreationType creationType = StateCreationType.AsNew)
            where TState : ITransitionState<TTrigger>
        {
            _stateCreations.Add(creationType);
            _stackOfThruResolvers.Add(TransitionStateGetterResolver<TState>);

            return this;
        }

        public void ToState<TState>(StateCreationType creationType = StateCreationType.AsNew)
            where TState : IState<TTrigger>
        {
            _info.TriggerType = typeof(TTrigger);
            _info.ThruResolveFunc = _stackOfThruResolvers;
            _info.ThruStateCreations = _stateCreations;
            _info.StateCreationType = creationType;
            _info.StateEnterResolveFunc = StateGetterResolver<TState>;

            _stateMachine.AddStateInfo(_info);
        }

        private ITransitionState TransitionStateGetterResolver<TState>(object trigger, Type prevState,
            StateCreationType stateCreationTypeType) where TState : ITransitionState<TTrigger>
        {
            ITransitionState<TTrigger> state =
                _stateFactory.CreateTransition<TTrigger>(typeof(TState), stateCreationTypeType);
            state.SetupExternalInfo((TTrigger)trigger, prevState);
            return state;
        }

        private IState<TTrigger> StateGetterResolver<TState>(object trigger, Type prevState,
            StateCreationType stateCreationTypeType) where TState : IState<TTrigger>
        {
            IState<TTrigger> state = _stateFactory.Create<TTrigger>(typeof(TState), stateCreationTypeType);
            state.SetupExternalInfo((TTrigger)trigger, prevState);
            return state;
        }

        public void Dispose()
        {
            _stackOfThruResolvers?.Clear();
            _stateCreations?.Clear();
            _info = default;
        }
    }
}