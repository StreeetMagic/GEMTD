using Games;
using InfastuctureCore.Services.StateMachineServices;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            EnterNextState();
        }

        public void Exit()
        {
            Debug.Log("Exited Bootstrap State");
        }

        private void EnterNextState() =>
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name == Constants.Scenes.InitialScene
                ? Constants.Scenes.Gameloop
                : SceneManager.GetActiveScene().name);
    }
}