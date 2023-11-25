using InfastuctureCore.SceneLoaders;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.States;
using UnityEngine;

namespace Games
{
    public class GameStateMachineData
    {
        public GameStateMachineData(MonoBehaviour coroutineRunner, string initialSceneName)
        {
            CoroutineRunner = coroutineRunner;
            SceneLoader = new SceneLoader(CoroutineRunner, initialSceneName);
        }

        public MonoBehaviour CoroutineRunner { get; }
        public SceneLoader SceneLoader { get; }

        public void RegisterStates(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            gameStateMachine.Register(new BootstrapState(gameStateMachine));
            gameStateMachine.Register(new LoadLevelState(gameStateMachine));
            gameStateMachine.Register(new GameLoopState(gameStateMachine));
        }
    }
}