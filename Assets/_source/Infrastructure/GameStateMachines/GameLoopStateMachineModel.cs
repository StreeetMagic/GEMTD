using InfastuctureCore.SceneLoaders;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.GameStateMachines.States;
using UnityEngine;

namespace Infrastructure.GameStateMachines
{
    public class GameStateMachineModel
    {
        public GameStateMachineModel(string initialSceneName)
        {
            SceneLoader = new SceneLoader(initialSceneName);
        }

        public SceneLoader SceneLoader { get; }

        public void RegisterStates(IStateMachineService<GameStateMachineModel> gameStateMachine, MonoBehaviour coroutineRunner)
        {
            gameStateMachine.Register(new BootstrapState(gameStateMachine, coroutineRunner));
            gameStateMachine.Register(new LoadLevelState(gameStateMachine, SceneLoader));
            gameStateMachine.Register(new GameLoopState());
            gameStateMachine.Register(new PrototypeState());
        }
    }
}