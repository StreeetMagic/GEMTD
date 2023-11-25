using Games;
using InfastuctureCore.Services.StateMachineServices;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;

        public GameLoopState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered GameLoop State");
        }

        public void Exit()
        {
            Debug.Log("Exited GameLoop State");
        }
    }
}