using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using modules.state_machine.trigger;

namespace modules.state_machine.state {

    public interface IAbstractState {}
    
    public interface IExitableState {
        UniTask Exit(CancellationToken cancellation_token);
    }

    public interface IEnterableState {
        void Enter();
    }

    public interface IBaseState : IExitableState, IEnterableState, IAbstractState {}

    public interface ITriggerableState<in TTrigger> where TTrigger : BaseStateTrigger {
        public void SetupExternalInfo(TTrigger trigger,  Type previous_state_type);
    }
    
    public interface IState<in TTrigger> : IBaseState, ITriggerableState<TTrigger> where TTrigger : BaseStateTrigger {}

    public interface ITransitionState : IAbstractState {
        public UniTask Thru(CancellationToken cancellation_token);
    }
    
    public interface ITransitionState<in TTrigger> : ITransitionState, ITriggerableState<TTrigger> where TTrigger : BaseStateTrigger {}
}
