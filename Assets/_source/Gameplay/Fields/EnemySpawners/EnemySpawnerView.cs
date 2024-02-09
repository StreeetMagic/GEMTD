using Gameplay.Fields.Enemies;
using Gameplay.Fields.EnemyContainers;
using Infrastructure.Services.GameFactories;
using UnityEngine;
using Zenject;

namespace Gameplay.Fields.EnemySpawners
{
  public class EnemySpawnerView : MonoBehaviour
  {
    private IGameFactoryService _gameFactoryService;
    public EnemyContainerView EnemyContainerView { get; set; }
    public EnemySpawnerModel EnemySpawnerModel { get; set; }

    [Inject]
    public void Construct(IGameFactoryService gameFactoryService)
    {
      _gameFactoryService = gameFactoryService;
    }

    public void Init(EnemySpawnerModel enemySpawnerModel)
    {
      EnemySpawnerModel = enemySpawnerModel;
      EnemySpawnerModel.EnemySpawned += OnEnemySpawned;
    }

    private void Awake()
    {
      EnemyContainerView = GetComponentInChildren<EnemyContainerView>();
    }

    private void OnDestroy()
    {
      EnemySpawnerModel.EnemySpawned -= OnEnemySpawned;
    }

    private void OnEnemySpawned(EnemyModel enemy)
    {
      Vector3 position = enemy.MoverModel.Position;
      EnemyView enemyView = _gameFactoryService.CreateEnemyView(position, enemy);
      enemyView.transform.SetParent(EnemyContainerView.transform);
    }
  }
}