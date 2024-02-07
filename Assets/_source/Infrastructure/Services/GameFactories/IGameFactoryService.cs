using Gameplay.Fields.Enemies;
using Infrastructure.Services.GameFactories.Factories;
using UnityEngine;

namespace Infrastructure.Services.GameFactories
{
  public interface IGameFactoryService : IService
  {
    FieldFactory FieldFactory { get; }

    UserInterfaceFactory UserInterfaceFactory { get; }

    void CreateProjectile(Transform shootingPoint, EnemyModel target);

    EnemyView CreateEnemyView(Vector3 position, EnemyModel model);

    EnemyModel CreateEnemyModel(Vector3 at, Vector2Int[] points, EnemyValues values, EnemiesConfig config);

    void CreateThrone(Vector3 position);
  }
}