using InfastuctureCore.SceneLoaders;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.GameStateMachines.States;
using UnityEngine;

namespace Infrastructure.GameStateMachines
{
    public class GameStateMachineModel
    {
        public GameStateMachineModel(MonoBehaviour coroutineRunner, string initialSceneName)
        {
            CoroutineRunner = coroutineRunner;
            SceneLoader = new SceneLoader(CoroutineRunner, initialSceneName);
        }

        public MonoBehaviour CoroutineRunner { get; }
        public SceneLoader SceneLoader { get; }

        public void RegisterStates(IStateMachineService<GameStateMachineModel> gameStateMachine, MonoBehaviour coroutineRunner)
        {
            gameStateMachine.Register(new BootstrapState(gameStateMachine, coroutineRunner));
            gameStateMachine.Register(new LoadLevelState(gameStateMachine));
            gameStateMachine.Register(new GameLoopState());
            gameStateMachine.Register(new PrototypeState());
        }
    }
}