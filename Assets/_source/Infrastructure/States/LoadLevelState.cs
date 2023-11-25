using System;
using Games;
using InfastuctureCore.SceneLoaders;
using InfastuctureCore.Services.StateMachineServices;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IState, IPayloadedState<string>
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;

            _sceneLoader = new SceneLoader(_gameStateMachine.Data.CoroutineRunner);
        }

        public void Enter()
        {
            Debug.Log("Entered LoadLevelState");

            _sceneLoader.Load(EnterState);
        }

        public void Enter(string sceneName)
        {
            Debug.Log("Entered LoadLevelState");

            _sceneLoader.Load(sceneName, EnterState);
        }

        private void EnterState(string name)
        {
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            Debug.Log("Exited LoadLevelState");
        }
    }
}

