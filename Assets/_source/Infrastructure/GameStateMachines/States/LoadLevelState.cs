using Games;
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

        public void Exit()
        {
            Debug.Log("Exited LoadLevelState");
        }

        private void OnSceneLoaded(string name)
        {
            if (name == Constants.Scenes.Prototype)
                _gameStateMachine.Enter<PrototypeState>();
            else
                _gameStateMachine.Enter<GameLoopState>();
        }
    }
}