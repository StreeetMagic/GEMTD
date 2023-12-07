using Gameplay.Fields.EnemySpawners.Enemies;
using Gameplay.Fields.EnemySpawners.EnemyContainers;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners
{
    public class EnemySpawnerView : MonoBehaviour
    {
        public EnemyContainerView EnemyContainerView { get; set; }
        public EnemySpawnerModel EnemySpawnerModel { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();

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
            EnemyView enemyView = GameFactoryService.CreateEnemyView(position, enemy);
            enemyView.transform.SetParent(EnemyContainerView.transform);
        }
    }
}