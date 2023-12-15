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

namespace Infrastructure.GameStateMachines.States
{
    public class GameLoopState : IGameStateMachineState
    {
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IAssetProviderService AssetProviderService => ServiceLocator.Instance.Get<IAssetProviderService>();

        public void Enter()
        {
            CurrentDataService.FieldModel = GameFactoryService.FieldFactory.CreateFieldModel();
            GameFactoryService.FieldFactory.CreateFieldView(CurrentDataService.FieldModel);
            GameFactoryService.FieldFactory.CreateCheckpointsDatas();
            GameFactoryService.FieldFactory.PaintBlocks();
            GameFactoryService.FieldFactory.CreateStartingLabyrinth();
            _gameLoopStateMachine = CreateGameLoopStateMachine();
            AssetProviderService.Instantiate<DebugHeadsUpDisplay>();
            _gameLoopStateMachine.Enter<PlaceWallsState>();
        }

        public void Exit()
        {
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