using Gameplay.Fields;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Walls.WallPlacers;
using Games;
using Infrastructure.Services.AssetProviderServices;
using Infrastructure.Services.CoroutineRunnerServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using Infrastructure.Services.InputServices;
using Infrastructure.Services.PoolServices;
using Infrastructure.Services.StateMachineServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using IStaticDataService = Infrastructure.Services.StaticDataServices.IStaticDataService;
using StaticDataService = Infrastructure.Services.StaticDataServices.StaticDataService;

namespace Infrastructure.GameStateMachines.States
{
  public class BootstrapState : IGameStateMachineState
  {
    private readonly IStateMachineService _gameStateMachine;
    private readonly MonoBehaviour _coroutineRunner;

    public BootstrapState(IStateMachineService gameStateMachine, MonoBehaviour coroutineRunner)
    {
      _gameStateMachine = gameStateMachine;
      _coroutineRunner = coroutineRunner;
    }

    private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

    public void Enter()
    {
      RegisterServices(_coroutineRunner);
      RegisterConfigs();
      EnterNextState();
    }

    public void Exit()
    {
    }

    private void RegisterServices(MonoBehaviour coroutineRunner)
    {
      var loc = ServiceLocator.Instance;

      var assetProvider = loc.Register<IAssetProviderService>(new AssetProviderService());
      var currentData = loc.Register<ICurrentDataService>(new CurrentDataService());
      var staticData = loc.Register<IStaticDataService>(new StaticDataService(assetProvider));
      loc.Register(_gameStateMachine);
      loc.Register<ICoroutineRunnerService>(new CoroutineRunnerService(coroutineRunner));
      loc.Register<IGameFactoryService>(new GameFactoryService(assetProvider, staticData, currentData));
    }

    private void RegisterConfigs()
    {
      StaticDataService.Register(new GameConfig());
      StaticDataService.Register(new CheckpointsConfig());
      StaticDataService.Register(new TowersConfig());
      StaticDataService.Register(new FieldConfig());
      StaticDataService.Register(new StartingLabyrinthConfig());
      StaticDataService.Register(new EnemiesConfig());
      StaticDataService.Register(new WallPlacerConfig());
    }

    private void EnterNextState() =>
      _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name == Constants.Scenes.InitialScene
        ? Constants.Scenes.Gameloop
        : SceneManager.GetActiveScene().name);
  }
}