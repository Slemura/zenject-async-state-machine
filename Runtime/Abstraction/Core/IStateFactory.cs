using System;
using RpDev.AsyncStateMachine.Resolver;
using RpDev.AsyncStateMachine.State;
using RpDev.AsyncStateMachine.Trigger;

namespace RpDev.AsyncStateMachine.Core
{
    public interface IStateFactory
    {
        IState<TTrigger> Create<TTrigger>(Type stateType, StateCreationType stateCreationType)
            where TTrigger : BaseStateTrigger;

        ITransitionState<TTrigger> CreateTransition<TTrigger>(Type stateType, StateCreationType stateCreationType)
            where TTrigger : BaseStateTrigger;
    }
}