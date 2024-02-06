using Debugs.Debug_HeadsUpDisplays;
using Gameplay.Players;
using Games;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using Infrastructure.Services.AssetProviderServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using Infrastructure.Services.StateMachineServices;
using Infrastructure.Services.StaticDataServices;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    public class GameLoopState : IGameStateMachineState
    {
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IAssetProviderService AssetProviderService => ServiceLocator.Instance.Get<IAssetProviderService>();
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public void Enter()
        {
            CurrentDataService.PlayerModel = new PlayerModel();
            CurrentDataService.FieldModel = GameFactoryService.FieldFactory.CreateFieldModel(StaticDataService.Get<GameConfig>());
            CurrentDataService.ThroneModel = GameFactoryService.FieldFactory.CreateThroneModel();
            GameFactoryService.FieldFactory.CreateFieldView(CurrentDataService.FieldModel);
            GameFactoryService.FieldFactory.CreateCheckpointsModels();
            GameFactoryService.FieldFactory.CreateStartingLabyrinth();
            GameFactoryService.CreateThrone(new Vector3(15, 0, 1));
            _gameLoopStateMachine = CreateGameLoopStateMachine();
            AssetProviderService.Instantiate<DebugHeadsUpDisplay>();
            _gameLoopStateMachine.Enter<PlaceWallsState>();
        }

        public void Exit()
        {
            _gameLoopStateMachine = null;
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