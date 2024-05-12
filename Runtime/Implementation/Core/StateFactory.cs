using System;
using System.Collections.Generic;
using RpDev.AsyncStateMachine.Resolver;
using RpDev.AsyncStateMachine.State;
using RpDev.AsyncStateMachine.Trigger;
using Zenject;

namespace RpDev.AsyncStateMachine.Core
{
    public class StateFactory : IStateFactory
    {
        private readonly IInstantiator _diContainer;
        private readonly Dictionary<Type, IAbstractState> _cachedStates = new Dictionary<Type, IAbstractState>();

        public StateFactory(IInstantiator diContainer)
        {
            _diContainer = diContainer;
        }

        public IState<TTrigger> Create<TTrigger>(Type stateType, StateCreationType stateCreationType)
            where TTrigger : BaseStateTrigger
        {
            switch (stateCreationType)
            {
                case StateCreationType.AsNew: return (IState<TTrigger>)_diContainer.Instantiate(stateType);

                case StateCreationType.AsCached:
                    if (_cachedStates.ContainsKey(stateType) == false)
                    {
                        _cachedStates.Add(stateType, (IBaseState)_diContainer.Instantiate(stateType));
                    }

                    return (IState<TTrigger>)_cachedStates[stateType];
                default: return (IState<TTrigger>)_diContainer.Instantiate(stateType);
            }
        }

        public ITransitionState<TTrigger> CreateTransition<TTrigger>(Type stateType,
            StateCreationType stateCreationType) where TTrigger : BaseStateTrigger
        {
            switch (stateCreationType)
            {
                case StateCreationType.AsNew: return (ITransitionState<TTrigger>)_diContainer.Instantiate(stateType);

                case StateCreationType.AsCached:
                    if (_cachedStates.ContainsKey(stateType) == false)
                    {
                        _cachedStates.Add(stateType, (IBaseState)_diContainer.Instantiate(stateType));
                    }

                    return (ITransitionState<TTrigger>)_cachedStates[stateType];
                default: return (ITransitionState<TTrigger>)_diContainer.Instantiate(stateType);
            }
        }
    }
}