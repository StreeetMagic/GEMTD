using Gameplay.Fields.Enemies;
using Gameplay.Fields.Thrones;
using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles;
using Games;
using Infrastructure.DIC;
using Infrastructure.Services.CoroutineRunners;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.GameFactories.Factories;
using Infrastructure.Services.StateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.ZenjectFactory;
using Infrastructure.Utilities;
using UnityEngine;
using IStaticDataService = Infrastructure.Services.StaticDataServices.IStaticDataService;

namespace Infrastructure.Services.GameFactories
{
  public class GameFactoryService : IGameFactoryService
  {
    private readonly IZenjectFactory _iZenjectFactory;

    public GameFactoryService(IZenjectFactory iZenjectFactory, IStaticDataService staticData,
      ICurrentDataService currentData, IGodFactory godFactory,
      IStateMachine<IGameLoopState> gameLoopStateMachine,
      ICoroutineRunner coroutineRunner)
    {
      _iZenjectFactory = iZenjectFactory;

      FieldFactory = new FieldFactory(_iZenjectFactory, staticData, currentData, this, godFactory, gameLoopStateMachine, coroutineRunner);
      UserInterfaceFactory = new UserInterfaceFactory(_iZenjectFactory);
    }

    public FieldFactory FieldFactory { get; }
    public UserInterfaceFactory UserInterfaceFactory { get; }

    public void CreateProjectile(Transform shootingPoint, EnemyModel target) =>
      _iZenjectFactory.Instantiate<DefaultProjectileView>(shootingPoint.position)
        .With(e => e.Init(new DefaultProjectileModel(target)));

    public EnemyModel CreateEnemyModel(Vector3 at, Vector2Int[] points, EnemyValues values, EnemiesConfig config) =>
      new EnemyModel(at, points, values, config);

    public void CreateThrone(Vector3 position)
    {
      _iZenjectFactory.Instantiate<ThroneView>(position);
    }

    public EnemyView CreateEnemyView(Vector3 position, EnemyModel model) =>
      _iZenjectFactory.Instantiate<EnemyView>(position)
        .With(e => e.Init(model));
  }
}