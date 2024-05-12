using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.AsyncStateMachine.Trigger;

namespace RpDev.AsyncStateMachine.State
{
    public abstract class BaseTriggerState<TTrigger> : ITriggerState<TTrigger> where TTrigger : BaseStateTrigger
    {
        protected TTrigger TriggerInfo { get; private set; }
        protected Type PrevStateType { get; private set; }

        public void SetupExternalInfo(TTrigger trigger, Type previousStateType)
        {
            PrevStateType = previousStateType;
            TriggerInfo = trigger;
        }
    }

    public abstract class BaseGameState<TTrigger> : BaseTriggerState<TTrigger>, IState<TTrigger>, IDisposable
        where TTrigger : BaseStateTrigger
    {
        public abstract void Enter();
        public virtual async UniTask Exit(CancellationToken cancellationToken) => await UniTask.CompletedTask;

        public virtual void Dispose()
        {
        }
    }

    public abstract class TransitionState<TTrigger> : BaseTriggerState<TTrigger>, ITransitionState<TTrigger>
        where TTrigger : BaseStateTrigger
    {
        public abstract UniTask Thru(CancellationToken cancellationToken);
    }
}