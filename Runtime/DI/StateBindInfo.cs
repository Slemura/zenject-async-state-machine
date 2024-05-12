using System;
using System.Collections.Generic;
using RpDev.AsyncStateMachine.State;

namespace RpDev.AsyncStateMachine.Resolver
{
    public struct StateBindInfo
    {
        public Type TriggerType;

        public IReadOnlyList<StateCreationType> ThruStateCreations;
        public StateCreationType StateCreationType;
        public Func<object, Type, StateCreationType, IBaseState> StateEnterResolveFunc;
        public IReadOnlyList<Func<object, Type, StateCreationType, ITransitionState>> ThruResolveFunc;
    }

    public enum StateCreationType
    {
        AsNew,
        AsCached
    }
}