using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Towers;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.GameFactories;
using Infrastructure.Services.StaticDataServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Fields.Walls.WallPlacers
{
  public class TowerPlacer
  {
    private readonly ICurrentDataService _currentDataService;
    private readonly IGameFactoryService _gameFactory;
    private readonly int _wallPlacementDelay = 100;

    private readonly IStaticDataService _staticDataService;

    public TowerPlacer(ICurrentDataService currentDataService, IStaticDataService staticDataService, IGameFactoryService gameFactory)
    {
      _currentDataService = currentDataService;
      _staticDataService = staticDataService;
      _gameFactory = gameFactory;
    }

    public async UniTask PlaceTowers(List<TowerType> towerTypes, List<int> levels)
    {
      List<Vector2Int> wallsCoordinates = GetWallCoordinates();
      RemovePlacedWalls(_currentDataService.FieldModel.RoundNumber - 1);

      await SetTowers(wallsCoordinates, towerTypes, levels);
    }

    public async UniTask ConfirmTower(CellModel cellModel)
    {
      List<Vector2Int> wallsCoordinates = GetWallCoordinates().ToList();

      await UniTask.Delay(_wallPlacementDelay);
      cellModel.ConfirmTower();
      wallsCoordinates.Remove(cellModel.Coordinates);

      await ReplaceTowersWithWalls(wallsCoordinates);
    }

    private List<Vector2Int> GetWallCoordinates() =>
      _currentDataService.FieldModel.RoundNumber <= _staticDataService.WallPlacerConfig.WallSettingsPerRounds.Count
        ? _staticDataService.WallPlacerConfig.WallSettingsPerRounds[_currentDataService.FieldModel.RoundNumber - 1].PlaceList
        : _currentDataService.FieldModel.GetCentralWalls(_staticDataService.WallPlacerConfig.TowerPerRound).ToList();

    private void RemovePlacedWalls(int roundIndex)
    {
      if (_currentDataService.FieldModel.RoundNumber >= _staticDataService.WallPlacerConfig.WallSettingsPerRounds.Count)
        return;

      if (_staticDataService.WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList.Count <= 0)
        return;

      foreach (Vector2Int coordinates in _staticDataService.WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList)
        _currentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).RemoveWallModel();
    }

    private async UniTask SetTowers(List<Vector2Int> wallsCoordinates, List<TowerType> towerTypes, List<int> levels)
    {
      for (var i = 0; i < wallsCoordinates.ToList().Count; i++)
      {
        Vector2Int vector2Int = wallsCoordinates.ToList()[i];

        int randomIndex = Random.Range(0, towerTypes.Count);
        TowerType randomTowerType = towerTypes[randomIndex];
        int randomLevel = levels[randomIndex];

        SetTower(vector2Int, randomTowerType, randomLevel);
        await UniTask.Delay(_wallPlacementDelay);
      }
    }

    private void SetTower(Vector2Int coordinatesValues, TowerType towerType, int level)
    {
      CellModel cellModel = _currentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues);

      if (cellModel.WallModel != null)
        cellModel.RemoveWallModel();

      cellModel.SetTowerModel(_gameFactory.FieldFactory.CreateTowerModel(towerType, level));
    }

    private async UniTask ReplaceTowersWithWalls(IEnumerable<Vector2Int> wallsCoordinates)
    {
      foreach (Vector2Int coordinates in wallsCoordinates)
      {
        CellModel cellModel = _currentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates);

        if (cellModel.TowerIsConfirmed == false)
          cellModel.RemoveTowerModel();

        await UniTask.Delay(_wallPlacementDelay);

        await PlaceWalls(coordinates);
      }
    }

    private async UniTask PlaceWalls(Vector2Int coordinates)
    {
      _currentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).SetWallModel(_gameFactory.FieldFactory.CreateWallModel());

      await UniTask.Delay(_wallPlacementDelay);
    }
  }
}