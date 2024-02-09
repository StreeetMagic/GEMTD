using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.EnemyContainers;
using Gameplay.Fields.PathFinders;
using Infrastructure.Services.CoroutineRunners;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.GameFactories;
using Infrastructure.Services.StaticDataServices;
using Infrastructure.Utilities;
using UnityEngine;

namespace Gameplay.Fields.EnemySpawners
{
  public class EnemySpawnerModel
  {
    private readonly int _count;
    private readonly ICurrentDataService _currentDataService;
    private readonly IGameFactoryService _gameFactoryService;
    private readonly IPathFinder _pathFinder = new BreadthFirstPathFinder();
    private readonly float _seconds;
    private readonly CoroutineDecorator _spawningCoroutine;
    private readonly IStaticDataService _staticDataService;

    public EnemySpawnerModel(EnemyContainerModel containerModel, float seconds, int count, ICoroutineRunner coroutineRunner, IStaticDataService staticDataService, IGameFactoryService gameFactoryService, ICurrentDataService currentDataService)
    {
      ContainerModel = containerModel;
      _seconds = seconds;
      _count = count;
      _staticDataService = staticDataService;
      _gameFactoryService = gameFactoryService;
      _currentDataService = currentDataService;
      _spawningCoroutine = new CoroutineDecorator(coroutineRunner as MonoBehaviour, Spawning);
    }

    private EnemyContainerModel ContainerModel { get; }

    public event Action<EnemyModel> EnemySpawned;

    public void Spawn(Action onComplete = null)
    {
      _spawningCoroutine.Start(onComplete);
    }

    private Vector2Int[] GetCheckPoints() =>
      _staticDataService.CheckpointsConfig.CheckpointValues.Select(checkPointModel => checkPointModel.Coordinates).ToArray();

    private IEnumerator Spawning(Action onComplete)
    {
      Vector2Int[] points = GetPoints();

      WaitForSeconds wait = new(_seconds);

      Vector2Int coordinates = _staticDataService.CheckpointsConfig.CheckpointValues[0].Coordinates;
      Vector3 startingPosition = new(coordinates.x, 0, coordinates.y);
      EnemiesConfig enemiesConfig = _staticDataService.EnemiesConfig;
      int roundNumber = _currentDataService.FieldModel.RoundNumber;

      for (var i = 0; i < _count; i++)
      {
        EnemyModel enemy = _gameFactoryService.CreateEnemyModel(startingPosition, points, enemiesConfig.Enemies[roundNumber], enemiesConfig);
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

      CellModel[] cells = _currentDataService.FieldModel.CellsContainerModel.CellModels;

      for (var i = 0; i < checkPoints.Length - 1; i++)
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