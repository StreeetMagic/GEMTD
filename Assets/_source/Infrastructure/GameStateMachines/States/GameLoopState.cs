using System.Collections;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.ServiceLocators.Utilities;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.Services.GameFactoryServices;
using Unity.VisualScripting;
using UnityEngine;
using IState = InfastuctureCore.Services.StateMachineServices.States.IState;

namespace Infrastructure.GameStateMachines.States
{
    public class GameLoopState : IState
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

        public GameLoopState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered GameLoop State");
            GameFactoryService.BlockGridFactory.CreateBlockGridData();
            GameFactoryService.LabyrinthFactory.CreateStartingLabyrinth();
            _gameLoopStateMachine = CreateGameLoopStateMachine();
            _gameLoopStateMachine.Enter<PlaceWallsState>();
        }

        private IStateMachineService<GameLoopStateMachineData> CreateGameLoopStateMachine()
        {
            var gameLoopStateMachineData = new GameLoopStateMachineData();
            var gameLoopStateMachine = new StateMachineService<GameLoopStateMachineData>(gameLoopStateMachineData);
            gameLoopStateMachineData.RegisterStates(gameLoopStateMachine);
            return gameLoopStateMachine;
        }

        public void Exit()
        {
            Debug.Log("Exited GameLoop State");
        }
    }
}