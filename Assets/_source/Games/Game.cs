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
            var gameStateMachineData = new GameStateMachineData(coroutineRunner, initialSceneName);
            var stateMachineService = new StateMachineService<GameStateMachineData>(gameStateMachineData);
            gameStateMachineData.RegisterStates(stateMachineService);
            stateMachineService.Enter<BootstrapState>();
        }
    }
}