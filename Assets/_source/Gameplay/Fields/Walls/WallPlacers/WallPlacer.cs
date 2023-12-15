using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Towers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Fields.Walls.WallPlacers
{
    public class TowerPlacer
    {
        private WallPlacerConfig WallPlacerConfig => ServiceLocator.Instance.Get<IStaticDataService>().Get<WallPlacerConfig>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void PlaceTowers(Action onComplete)
        {
            List<Vector2Int> wallsCoordinates = GetWallCoordinates();

            PlaceNewWalls(CurrentDataService.FieldModel.RoundNumber - 1);
            SetTowers(wallsCoordinates);
            ConfirmRandomTower(wallsCoordinates);
            RemoveTowers(wallsCoordinates);
            PlaceWalls(wallsCoordinates);

            Debug.Log("Закончил ставить башни");
            onComplete?.Invoke();
        }

        private List<Vector2Int> GetWallCoordinates() =>
            CurrentDataService.FieldModel.RoundNumber <= WallPlacerConfig.WallSettingsPerRounds.Count
                ? WallPlacerConfig.WallSettingsPerRounds[CurrentDataService.FieldModel.RoundNumber - 1].PlaceList
                : CurrentDataService.FieldModel.GetCentralWalls(WallPlacerConfig.towerPerRound).ToList();

        private void PlaceNewWalls(int roundIndex)
        {
            if (CurrentDataService.FieldModel.RoundNumber >= WallPlacerConfig.WallSettingsPerRounds.Count)
                return;

            if (WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList.Count <= 0)
                return;

            foreach (Vector2Int coordinates in WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList)
                CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).RemoveWallModel();
        }

        private void SetTowers(List<Vector2Int> wallsCoordinates) =>
            wallsCoordinates.ToList().ForEach(SetTower);

        private void SetTower(Vector2Int coordinatesValues)
        {
            if (CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues).WallModel != null)
                CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues).RemoveWallModel();

            CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues).SetTowerModel(GameFactory.FieldFactory.CreateTowerData((TowerType)Random.Range(0, 8), 1));
        }

        private void ConfirmRandomTower(IReadOnlyList<Vector2Int> wallsCoordinates) =>
            CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(wallsCoordinates[Random.Range(0, wallsCoordinates.Count)]).ConfirmTower();

        private void RemoveTowers(IEnumerable<Vector2Int> wallsCoordinates) =>
            wallsCoordinates
                .Where(coordinates => !CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).TowerIsConfirmed)
                .ForEach(coordinates => CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).RemoveTowerModel());

        private void PlaceWalls(IEnumerable<Vector2Int> wallsCoordinates) =>
            wallsCoordinates
                .Where(coordinates => !CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).TowerIsConfirmed)
                .ForEach(coordinates => CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).SetWallModel(GameFactory.FieldFactory.CreateWallData()));
    }
}