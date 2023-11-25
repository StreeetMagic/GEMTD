using InfastuctureCore.SceneLoaders;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.States;
using UnityEngine;

namespace Games
{
    public class Game
    {
        public Game(MonoBehaviour coroutineRunner, string initialSceneName)
        {
            var gameStateMachineData = new GameStateMachineData(coroutineRunner, initialSceneName);
            var stateMachineService = new StateMachineService<GameStateMachineData>(gameStateMachineData);
            gameStateMachineData.RegisterStates(stateMachineService);
            stateMachineService.Enter<BootstrapState>();
        }
    }
}