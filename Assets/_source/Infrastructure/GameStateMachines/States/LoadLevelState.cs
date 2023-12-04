using System;
using InfastuctureCore.SceneLoaders;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
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

        public event Action<IState> Entered;
        public event Action<IExitableState> Exited;

        public void Enter(string sceneName)
        {
            Debug.Log("Entered LoadLevelState");

            _sceneLoader.Load(sceneName, OnSceneLoaded);
        }

        public void Enter()
        {
            Debug.Log("Entered LoadLevelState");

            _sceneLoader.Load(OnSceneLoaded);
        }

        private void OnSceneLoaded(string name)
        {
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            Debug.Log("Exited LoadLevelState");
        }
    }
}