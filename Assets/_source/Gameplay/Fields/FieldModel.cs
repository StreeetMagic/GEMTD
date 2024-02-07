using System.Collections.Generic;
using Gameplay.Fields.Cells;
using Gameplay.Fields.CellsContainers;
using Gameplay.Fields.EnemySpawners;
using Infrastructure;
using Infrastructure.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields
{
  public class FieldModel
  {
    private readonly IStaticDataService _staticDataService;

    public FieldModel(CellModel[] cellmodels, EnemySpawnerModel enemyEnemySpawnerModel, IStaticDataService staticDataService)
    {
      EnemySpawnerModel = enemyEnemySpawnerModel;
      _staticDataService = staticDataService;
      CellsContainerModel = new CellsContainerModel(cellmodels);
    }

    public CellsContainerModel CellsContainerModel { get; }
    public EnemySpawnerModel EnemySpawnerModel { get; }
    public int RoundNumber { get; set; } = 0;

    public Vector2Int[] GetCentralWalls(int towerPerRound)
    {
      int size = _staticDataService.FieldConfig.FieldSize;
      int centralCoordinate = size / 2 + 1;
      List<Vector2Int> coordinates = new List<Vector2Int>();

      while (coordinates.Count < towerPerRound)
      {
        for (int i = 0; i < centralCoordinate; i++)
        {
          FindValidCoordinates(centralCoordinate, i, coordinates, towerPerRound);

          if (coordinates.Count == towerPerRound)
          {
            if (HasSameCoordinates(coordinates.ToArray()))
            {
              coordinates.Clear();
            }
            else
            {
              return coordinates.ToArray();
            }
          }
        }
      }

      return coordinates.ToArray();
    }

    private void FindValidCoordinates(int centralCoordinate, int i, List<Vector2Int> coordinates, int towerPerRound)
    {
      for (int x = centralCoordinate - i; x < centralCoordinate + i; x++)
      {
        for (int z = centralCoordinate - i; z < centralCoordinate + i; z++)
        {
          CellModel cellModel = CellsContainerModel.GetCellModel(new Vector2Int(x, z));

          if (cellModel.CanBeReplacedWithTower)
          {
            coordinates.Add(new Vector2Int(x, z));
          }

          if (coordinates.Count == towerPerRound)
          {
            return;
          }
        }
      }
    }

    private bool HasSameCoordinates(Vector2Int[] coordinates)
    {
      for (int i = 0; i < coordinates.Length; i++)
      {
        for (int j = i + 1; j < coordinates.Length; j++)
        {
          if (coordinates[i].Equals(coordinates[j]))
          {
            return true;
          }
        }
      }

      return false;
    }
  }
}