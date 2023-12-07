using System;
using System.Collections;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.EnemySpawners.Enemies;
using Gameplay.Fields.EnemySpawners.EnemyContainers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.CoroutineRunnerServices;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners
{
    public class EnemySpawnerModel
    {
        private CoroutineDecorator _spawningCoroutine;

        public EnemySpawnerModel(EnemyContainerModel containerModel)
        {
            ContainerModel = containerModel;
            _spawningCoroutine = new CoroutineDecorator(CoroutineRunner, Spawning);
        }

        public event Action<EnemyModel> EnemySpawned;

        public EnemyContainerModel ContainerModel { get; }

        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<ICoroutineRunnerService>().Instance;
        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public void Spawn(Action onComplete)
        {
            _spawningCoroutine.Start(onComplete);
        }

        private IEnumerator Spawning(Action onComplete)
        {
            WaitForSeconds wait = new(0.5f);
            int count = 15;

            CoordinatesValues coordinates = StaticDataService.Get<CheckpointsConfig>().CheckPointSettings[0].CoordinatesValues;
            Vector3 startingPosition = new(coordinates.X, 0, coordinates.Z);

            for (int i = 0; i < count; i++)
            {
                EnemyModel enemy = GameFactoryService.CreateEnemyModel(startingPosition);
                ContainerModel.Enemies.Add(enemy);
                EnemySpawned?.Invoke(enemy);
                yield return wait;
            }

            onComplete?.Invoke();
        }
    }
}