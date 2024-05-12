using System;

namespace RpDev.AsyncStateMachine.Core
{
    public interface IStateTriggerDispatcher : IDisposable
    {
        void StateTrigger<TBaseStateTrigger>();
        void StateTrigger(object triggerInstance);
    }
}