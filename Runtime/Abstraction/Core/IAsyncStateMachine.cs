using System;
using RpDev.AsyncStateMachine.Resolver;

namespace RpDev.AsyncStateMachine.Core
{
    public interface IAsyncStateMachine : IDisposable
    {
        void EnterStateBeTriggerType(object trigger);
        void AddStateInfo(StateBindInfo info);
    }
}