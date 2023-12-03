using GameDesign;
using Gameplay.Fields;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.WallPlacers;
using Games;
using Games.Config.Resources;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.PoolServices;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using Infrastructure.Services.InputServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using IStaticDataService = InfastuctureCore.Services.StaticDataServices.IStaticDataService;
using StaticDataService = InfastuctureCore.Services.StaticDataServices.StaticDataService;

namespace Infrastructure.GameStateMachines.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;

        public BootstrapState(IStateMachineService<GameStateMachineData> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public void Enter()
        {
            Debug.Log("Entered Bootstrap State");

            RegisterServices();
            RegisterConfigs(ServiceLocator.Instance.Get<IAssetProviderService>());
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
            locator.Register<IInputService>(new InputService());
            locator.Register<IPoolRepositoryService>(new PoolRepositoryService());
            locator.Register<IGameFactoryService>(new GameFactoryService(assetProvider, staticData, currentData));
        }

        private void RegisterConfigs(IAssetProviderService assetProvider)
        {
            StaticDataService.Register(assetProvider.InstantiateScriptableObject<GameConfig>());
            StaticDataService.Register(assetProvider.InstantiateScriptableObject<FieldConfig>());
            StaticDataService.Register(assetProvider.InstantiateScriptableObject<CheckpointsConfig>());
            StaticDataService.Register(assetProvider.InstantiateScriptableObject<MapWallsConfig>());
            StaticDataService.Register(assetProvider.InstantiateScriptableObject<StartingLabyrinthConfig>());
            StaticDataService.Register(assetProvider.InstantiateScriptableObject<PaintedBlockConfig>());
            StaticDataService.Register(assetProvider.InstantiateScriptableObject<WallPlacerConfig>());
        }

        private void EnterNextState() =>
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name == Constants.Scenes.InitialScene
                ? Constants.Scenes.Gameloop
                : SceneManager.GetActiveScene().name);
    }
}