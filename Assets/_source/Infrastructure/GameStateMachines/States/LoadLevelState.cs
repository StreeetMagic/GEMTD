using Games;
using Infrastructure.SceneLoaders;
using Infrastructure.Services.StateMachineServices;
using Infrastructure.Services.StateMachineServices.States;

namespace Infrastructure.GameStateMachines.States
{
    public class LoadLevelState : IGameStateMachineState, IPayloadedState<string>
    {
        private readonly IStateMachineService<GameStateMachineModel> _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(IStateMachineService<GameStateMachineModel> gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnSceneLoaded);
        }

        public void Enter()
        {
            _sceneLoader.Load(OnSceneLoaded);
        }

        public void Exit()
        {
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