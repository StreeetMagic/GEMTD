using Gameplay.Fields.Walls.WallPlacers;
using Games;
using Infrastructure.Services.AssetProviders;
using Infrastructure.Services.CoroutineRunners;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.GameFactories;
using Infrastructure.Services.Inputs;
using Infrastructure.Services.StateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachines.GameStateMachines.States;
using Infrastructure.Services.StateMachines.StateFactories;
using Infrastructure.Services.StaticDataServices;
using Infrastructure.Services.ZenjectFactory;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.DIC
{
  public class GameInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindInitialSceneName();
      BindCoroutineRunner();
      BindZenjectFactory();

      BindStateFactory();
      BindInfrastructureFactory();

      BindGameStateMachine();
      BindGameLoopStateMachine();

      BindTowerPlacer();

      BindInput();
      BindAssetProvider();
      BindCurrentDataService();
      BindStaticDataService();
      BindGameFactoryService();
    }

    private void BindZenjectFactory() =>
      Container
        .Bind<IZenjectFactory>()
        .To<ZenjectFactory>()
        .AsSingle();

    private void BindTowerPlacer()
    {
      Container
        .Bind<TowerPlacer>()
        .AsSingle();
    }

    private void BindCoroutineRunner()
    {
      Container
        .Bind<ICoroutineRunner>()
        .To<CoroutineRunner>()
        .FromComponentInNewPrefabResource(Constants.AssetsPath.Prefabs.CoroutineRunner)
        .AsSingle();
    }

    private void BindGameFactoryService() =>
      Container
        .Bind<IGameFactoryService>()
        .To<GameFactoryService>()
        .AsSingle();

    private void BindStaticDataService() =>
      Container
        .Bind<IStaticDataService>()
        .To<StaticDataService>()
        .AsSingle();

    private void BindCurrentDataService() =>
      Container
        .Bind<ICurrentDataService>()
        .To<CurrentDataService>()
        .AsSingle();

    private void BindAssetProvider() =>
      Container
        .Bind<IAssetProvider>()
        .To<AssetProvider>()
        .AsSingle();

    private void BindInfrastructureFactory() =>
      Container
        .Bind<IGodFactory>()
        .To<GodFactory>()
        .AsSingle();

    private void BindInitialSceneName() =>
      Container
        .Bind<string>()
        .WithId(Constants.Ids.InitialSceneName)
        .FromInstance(SceneManager.GetActiveScene().name);

    private void BindStateFactory() =>
      Container
        .Bind<IStateFactory>()
        .To<StateFactory>()
        .AsSingle();

    private void BindInput() =>
      Container
        .Bind<IInputService>()
        .To<InputService>()
        .AsSingle();

    private void BindGameStateMachine() =>
      Container
        .Bind<IStateMachine<IGameState>>()
        .To<StateMachine<IGameState>>()
        .AsSingle();

    private void BindGameLoopStateMachine() =>
      Container
        .Bind<IStateMachine<IGameLoopState>>()
        .To<StateMachine<IGameLoopState>>()
        .AsSingle();
  }
}