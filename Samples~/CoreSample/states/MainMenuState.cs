using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.AsyncStateMachine.Core;
using RpDev.AsyncStateMachine.State;
using UnityEngine;

namespace RpDev.AsyncStateMachine.Sample.States
{
    public class MainMenuState : BaseGameState<SampleTriggers.MainMenuTrigger>
    {
        private readonly CallCounter _counter;
        private readonly IStateTriggerDispatcher _stateDispatcher;

        public MainMenuState(CallCounter counter, IStateTriggerDispatcher stateDispatcher)
        {
            _counter = counter;
            _stateDispatcher = stateDispatcher;
        }

        public override void Enter()
        {
            Logger.Log($"{_counter.Count} ===MainMenuState=== {PrevStateType?.Name}");
            _counter.Increment();
            _stateDispatcher.StateTrigger(new SampleTriggers.StartLevelTrigger
            {
                LevelNum = 5
            });
        }

        public override UniTask Exit(CancellationToken cancellationToken)
        {
            Logger.Log($"{_counter.Count}---Exit from MainMenuState", Color.red);
            _counter.Increment();
            return base.Exit(cancellationToken);
        }
    }
}