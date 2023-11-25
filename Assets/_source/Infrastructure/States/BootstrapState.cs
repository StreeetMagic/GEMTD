using Games;
using InfastuctureCore.Services.StateMachineServices;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;

        public BootstrapState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered Bootstrap State");

            _gameStateMachine.Enter<LoadLevelState, string>(Constants.Scenes.Gameloop);
        }

        public void Exit()
        {
            Debug.Log("Exited Bootstrap State");
        }
    }
}