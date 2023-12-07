using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using UnityEngine;

namespace Games
{
    public class Game
    {
        public Game(MonoBehaviour coroutineRunner, string initialSceneName)
        {
            var gameStateMachineData = new GameStateMachineModel(coroutineRunner, initialSceneName);
            var stateMachineService = new StateMachineService<GameStateMachineModel>(gameStateMachineData);
            gameStateMachineData.RegisterStates(stateMachineService, coroutineRunner);
            stateMachineService.Enter<BootstrapState>();
        }
    }
}