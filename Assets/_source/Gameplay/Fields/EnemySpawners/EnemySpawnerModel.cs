using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.EnemyContainers;
using Gameplay.Fields.PathFinders;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.CoroutineRunnerServices;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners
{
    public class EnemySpawnerModel
    {
        private readonly CoroutineDecorator _spawningCoroutine;
        private readonly IPathFinder _pathFinder = new BreadthFirstPathFinder();

        public EnemySpawnerModel(EnemyContainerModel containerModel)
        {
            ContainerModel = containerModel;
            _spawningCoroutine = new CoroutineDecorator(CoroutineRunner, Spawning);
        }

        public event Action<EnemyModel> EnemySpawned;

        private EnemyContainerModel ContainerModel { get; }

        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<ICoroutineRunnerService>().Instance;
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();
        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public void Spawn(Action onComplete = null)
        {
            _spawningCoroutine.Start(onComplete);
        }

        private Vector2Int[] GetCheckPoints() =>
            StaticDataService.Get<CheckpointsConfig>().CheckpointValues.Select(checkPointModel => checkPointModel.Coordinates).ToArray();

        private IEnumerator Spawning(Action onComplete)
        {
            Vector2Int[] points = GetPoints();

            WaitForSeconds wait = new(0.5f);
            int count = 10;

            Vector2Int coordinates = StaticDataService.Get<CheckpointsConfig>().CheckpointValues[0].Coordinates;
            Vector3 startingPosition = new(coordinates.x, 0, coordinates.y);
            EnemiesConfig enemiesConfig = StaticDataService.Get<EnemiesConfig>();
            int roundNumber = CurrentDataService.FieldModel.RoundNumber;

            for (int i = 0; i < count; i++)
            {
                EnemyModel enemy = GameFactoryService.CreateEnemyModel(startingPosition, points, enemiesConfig.Enemies[roundNumber]);
                ContainerModel.AddEnemy(enemy);
                EnemySpawned?.Invoke(enemy);
                yield return wait;
            }

            onComplete?.Invoke();
        }

        private Vector2Int[] GetPoints()
        {
            Vector2Int[] checkPoints = GetCheckPoints();

            List<Vector2Int> foundPoints = new();

            CellModel[] cells = CurrentDataService.FieldModel.CellsContainerModel.CellModels;

            for (int i = 0; i < checkPoints.Length - 1; i++)
            {
                _pathFinder.FindPath(cells, checkPoints[i], checkPoints[i + 1], foundPoints);
            }

            List<Vector2Int> vectorPoints = new();

            foreach (Vector2Int point in foundPoints)
            {
                var vector2Int = new Vector2Int(point.x, point.y);
                vectorPoints.Add(vector2Int);
            }

            return vectorPoints.ToArray();
        }
    }
}