using System;
using Debug_HeadsUpDisplays;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using IState = InfastuctureCore.Services.StateMachineServices.States.IState;

namespace Infrastructure.GameStateMachines.States
{
    public class GameLoopState : IState
    {
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        public event Action<IState> Entered;
        public event Action<IExitableState> Exited;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IAssetProviderService AssetProviderService => ServiceLocator.Instance.Get<IAssetProviderService>();

        public void Enter()
        {
            Debug.Log("Entered GameLoop State");
            CurrentDataService.FieldData = GameFactoryService.BlockGridFactory.CreateFieldData();
            GameFactoryService.BlockGridFactory.CreateBlockGridView(CurrentDataService.FieldData);
            GameFactoryService.BlockGridFactory.CreateCheckpointsDatas();
            GameFactoryService.BlockGridFactory.PaintBlocks();
            GameFactoryService.LabyrinthFactory.CreateStartingLabyrinth();
            _gameLoopStateMachine = CreateGameLoopStateMachine();
            AssetProviderService.Instantiate<DebugHeadsUpDisplay>();
            _gameLoopStateMachine.Enter<PlaceWallsState>();
        }

        public void Exit()
        {
            Debug.Log("Exited GameLoop State");
        }

        private IStateMachineService<GameLoopStateMachineData> CreateGameLoopStateMachine()
        {
            var gameLoopStateMachineData = new GameLoopStateMachineData();
            var gameLoopStateMachine = new StateMachineService<GameLoopStateMachineData>(gameLoopStateMachineData);
            gameLoopStateMachineData.RegisterStates(gameLoopStateMachine);
            ServiceLocator.Instance.Register<IStateMachineService<GameLoopStateMachineData>>(gameLoopStateMachine);

            return gameLoopStateMachine;
        }
    }
}