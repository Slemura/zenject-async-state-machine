using RpDev.AsyncStateMachine.Trigger;
using Zenject;

namespace RpDev.AsyncStateMachine.Resolver
{
    public static class StateExtension
    {
        public static StateBinder<TTransition> BindStateTrigger<TTransition>(this DiContainer container)
            where TTransition : BaseStateTrigger, new()
        {
            return container.Instantiate<StateBinder<TTransition>>();
        }
    }
}