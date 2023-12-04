using System;
using GameDesign;
using Gameplay.Fields;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.Towers;
using Gameplay.Fields.WallPlacers;
using Games;
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

        public event Action<IState> Entered;
        public event Action<IExitableState> Exited;

        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

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
            var loc = ServiceLocator.Instance;

            var assetProvider = loc.Register<IAssetProviderService>(new AssetProviderService());
            var currentData = loc.Register<ICurrentDataService>(new CurrentDataService());
            var staticData = loc.Register<IStaticDataService>(new StaticDataService(assetProvider));
            var stateMachine = loc.Register<IStateMachineService<GameStateMachineData>>(_gameStateMachine);
            var input = loc.Register<IInputService>(new InputService());
            var poolRepository = loc.Register<IPoolRepositoryService>(new PoolRepositoryService());
            var gameFactory = loc.Register<IGameFactoryService>(new GameFactoryService(assetProvider, staticData, currentData));
        }

        private void RegisterConfigs()
        {
            StaticDataService.Register<GameConfig>();
            StaticDataService.Register<FieldConfig>();
            StaticDataService.Register<CheckpointsConfig>();
            StaticDataService.Register<MapWallsConfig>();
            StaticDataService.Register<StartingLabyrinthConfig>();
            StaticDataService.Register<PaintedBlockConfig>();
            StaticDataService.Register<WallPlacerConfig>();
            StaticDataService.Register<TowerConfig>();
        }

        private void EnterNextState() =>
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name == Constants.Scenes.InitialScene
                ? Constants.Scenes.Gameloop
                : SceneManager.GetActiveScene().name);
    }
}