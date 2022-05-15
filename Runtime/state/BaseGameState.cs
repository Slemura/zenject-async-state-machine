using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.trigger;

namespace modules.state_machine.state {

    public abstract class BaseTriggerableState<TTrigger> : ITriggerableState<TTrigger> where TTrigger : BaseStateTrigger {
        protected TTrigger trigger_info;
        
        public void SetupTriggerInfo(TTrigger trigger) {
            trigger_info = trigger;
        }
    }

    public abstract class BaseGameState<TTrigger> : BaseTriggerableState<TTrigger>, IState<TTrigger>, IDisposable where TTrigger : BaseStateTrigger {
        public abstract      void    Enter();
        public virtual async UniTask Exit(CancellationToken cancellation_token) => await UniTask.CompletedTask;
        
        public virtual void Dispose() { }
    }

    public abstract class TransitionState<TTrigger> : BaseTriggerableState<TTrigger>, ITransitionState<TTrigger> where TTrigger : BaseStateTrigger {
        public abstract UniTask Thru(CancellationToken cancellation_token);
    }
}