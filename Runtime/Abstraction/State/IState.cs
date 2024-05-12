using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.AsyncStateMachine.Trigger;

namespace RpDev.AsyncStateMachine.State
{
    public interface IAbstractState
    {
    }

    public interface IExitState
    {
        UniTask Exit(CancellationToken cancellationToken);
    }

    public interface IEnterState
    {
        void Enter();
    }

    public interface IBaseState : IExitState, IEnterState, IAbstractState
    {
    }

    public interface ITriggerState<in TTrigger> where TTrigger : BaseStateTrigger
    {
        public void SetupExternalInfo(TTrigger trigger, Type previousStateType);
    }

    public interface IState<in TTrigger> : IBaseState, ITriggerState<TTrigger> where TTrigger : BaseStateTrigger
    {
    }

    public interface ITransitionState : IAbstractState
    {
        public UniTask Thru(CancellationToken cancellationToken);
    }

    public interface ITransitionState<in TTrigger> : ITransitionState, ITriggerState<TTrigger>
        where TTrigger : BaseStateTrigger
    {
    }
}