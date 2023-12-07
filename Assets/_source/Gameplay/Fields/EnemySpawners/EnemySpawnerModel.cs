using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.EnemySpawners.Enemies;
using Gameplay.Fields.EnemySpawners.EnemyContainers;
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
        private CoroutineDecorator _spawningCoroutine;
        private IPathFinder _pathFinder = new BreadthFirstPathFinder();

        public EnemySpawnerModel(EnemyContainerModel containerModel)
        {
            ContainerModel = containerModel;
            _spawningCoroutine = new CoroutineDecorator(CoroutineRunner, Spawning);
        }

        public event Action<EnemyModel> EnemySpawned;

        public EnemyContainerModel ContainerModel { get; }

        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<ICoroutineRunnerService>().Instance;
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();
        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public void Spawn(Action onComplete)
        {
            _spawningCoroutine.Start(onComplete);
        }

        private CoordinatesValues[] GetCheckPoints()
        {
            CheckPointModel[] positions = CurrentDataService.FieldModel.CellsContainerModel.GetCheckPointModels();

            List<CoordinatesValues> checkPoints = new();

            foreach (CheckPointModel checkPointModel in positions)
            {
                checkPoints.Add(checkPointModel.CellModel.CoordinatesValues);
            }

            return checkPoints.ToArray();
        }

        private IEnumerator Spawning(Action onComplete)
        {
            Vector3[] points = GetPoints();

            WaitForSeconds wait = new(0.5f);
            int count = 15;

            CoordinatesValues coordinates = StaticDataService.Get<CheckpointsConfig>().CheckPointSettings[0].CoordinatesValues;
            Vector3 startingPosition = new(coordinates.X, 0, coordinates.Z);

            for (int i = 0; i < count; i++)
            {
                EnemyModel enemy = GameFactoryService.CreateEnemyModel(startingPosition, points);
                ContainerModel.Enemies.Add(enemy);
                EnemySpawned?.Invoke(enemy);
                yield return wait;
            }

            onComplete?.Invoke();
        }

        private Vector3[] GetPoints()
        {
            CoordinatesValues[] checkPoints = GetCheckPoints();
            List<CoordinatesValues> points = new();
            CellModel[] cells = CurrentDataService.FieldModel.CellsContainerModel.CellModels;

            for (int i = 0; i < checkPoints.Length - 1; i++)
            {
                CoordinatesValues[] localPoints = _pathFinder.FindPath(cells, checkPoints[i], checkPoints[i + 1]);

                foreach (var point in localPoints)
                {
                    points.Add(point);
                }
            }

            List<Vector3> vectorPoints = new();

            foreach (var point in points)
            {
                vectorPoints.Add(new Vector3(point.X, 0, point.Z));
            }

            return vectorPoints.ToArray();
        }
    }
}