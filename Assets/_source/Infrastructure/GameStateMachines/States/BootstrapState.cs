using Gameplay.BlockGrids;
using Gameplay.BlockGrids.Checkpoints;
using Games;
using Games.Config.Resources;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.PoolServices;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using Infrastructure.Services.StaticDataServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.GameStateMachines.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;

        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public BootstrapState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entered Bootstrap State");

            RegisterServices();
            RegisterConfigs();
            EnterNextState();
        }

        public void Exit()
        {
            Debug.Log("Exited Bootstrap State");
        }

        private void RegisterServices()
        {
            var locator = ServiceLocator.Instance;

            var staticData = locator.Register<IStaticDataService>(new StaticDataService());
            var assetProvider = locator.Register<IAssetProviderService>(new AssetProviderService());
            var currentData = locator.Register<ICurrentDataService>(new CurrentDataService());
            locator.Register(_gameStateMachine);
            locator.Register<IPoolRepositoryService>(new PoolRepositoryService());
            locator.Register<IGameFactoryService>(new GameFactoryService(assetProvider, staticData, currentData));
        }

        private void RegisterConfigs()
        {
            StaticDataService.Register(Resources.Load<GameConfig>(Constants.AssetsPath.Configs.GameConfig));
            StaticDataService.Register(Resources.Load<BlockGridConfig>(Constants.AssetsPath.Configs.BlockGridConfig));
            StaticDataService.Register(Resources.Load<CheckpointsConfig>(Constants.AssetsPath.Configs.CheckpointsConfig));
            StaticDataService.Register(Resources.Load<MapWallsConfig>(Constants.AssetsPath.Configs.MapWallsConfig));
            StaticDataService.Register(Resources.Load<StartingLabyrinthConfig>(Constants.AssetsPath.Configs.StartingLabyrinthConfig));
        }

        private void EnterNextState() =>
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name == Constants.Scenes.InitialScene
                ? Constants.Scenes.Gameloop
                : SceneManager.GetActiveScene().name);
    }
}