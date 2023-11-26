using Games;
using Games.Config.Resources;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.PoolServices;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachineService<GameStateMachineData> _gameStateMachine;

        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();
        private IAssetProviderService AssetProviderService => ServiceLocator.Instance.Get<IAssetProviderService>();

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

            var gsm = locator.Register<IStateMachineService<GameStateMachineData>>(_gameStateMachine);
            var staticData = locator.Register<IStaticDataService>(new StaticDataService());
            var assetProvider = locator.Register<IAssetProviderService>(new AssetProviderService());
            var poolRep = locator.Register<IPoolRepositoryService>(new PoolRepositoryService());
            var currentData = locator.Register<ICurrentDataService>(new CurrentDataService());
            var gameFactory = locator.Register<IGameFactoryService>(new GameFactoryService(assetProvider, staticData, currentData));
        }

        private void RegisterConfigs()
        {
            StaticDataService.Register(Resources.Load<GameConfig>(Constants.AssetsPath.Configs.GameConfig));
        }

        private void EnterNextState() =>
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name == Constants.Scenes.InitialScene
                ? Constants.Scenes.Gameloop
                : SceneManager.GetActiveScene().name);
    }
}