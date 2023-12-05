﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Towers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using Sirenix.Utilities;
using Random = UnityEngine.Random;

namespace Gameplay.Walls.WallPlacers
{
    public class TowerPlacer
    {
        private WallPlacerConfig WallPlacerConfig => ServiceLocator.Instance.Get<IStaticDataService>().Get<WallPlacerConfig>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void PlaceTowers(Action onComplete)
        {
            List<CoordinatesValues> wallsCoordinates = GetWallCoordinates();

            PlaceNewWalls(CurrentDataService.FieldModel.RoundNumber - 1);
            SetTowers(wallsCoordinates);
            ConfirmRandomTower(wallsCoordinates);
            RemoveTowers(wallsCoordinates);
            PlaceWalls(wallsCoordinates);

            onComplete?.Invoke();
        }

        private List<CoordinatesValues> GetWallCoordinates() =>
            CurrentDataService.FieldModel.RoundNumber < WallPlacerConfig.WallSettingsPerRounds.Count
                ? WallPlacerConfig.WallSettingsPerRounds[CurrentDataService.FieldModel.RoundNumber - 1].PlaceList
                : CurrentDataService.FieldModel.GetCentralWalls(WallPlacerConfig.towerPerRound).ToList();

        private void PlaceNewWalls(int roundIndex)
        {
            if (CurrentDataService.FieldModel.RoundNumber >= WallPlacerConfig.WallSettingsPerRounds.Count)
                return;

            if (WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList.Count <= 0)
                return;

            foreach (CoordinatesValues coordinates in WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList)
                CurrentDataService.FieldModel.GetCellData(coordinates).RemoveWallData();
        }

        private void SetTowers(List<CoordinatesValues> wallsCoordinates) =>
            wallsCoordinates.ToList().ForEach(SetTower);

        private void SetTower(CoordinatesValues coordinatesValues)
        {
            if (CurrentDataService.FieldModel.GetCellData(coordinatesValues).WallData != null)
                CurrentDataService.FieldModel.GetCellData(coordinatesValues).RemoveWallData();

            CurrentDataService.FieldModel.GetCellData(coordinatesValues).SetTowerData(GameFactory.FieldFactory.CreateTowerData((TowerType)Random.Range(0, 8), 1));
        }

        private void ConfirmRandomTower(IReadOnlyList<CoordinatesValues> wallsCoordinates) =>
            CurrentDataService.FieldModel.GetCellData(wallsCoordinates[Random.Range(0, wallsCoordinates.Count)]).ConfirmTower();

        private void RemoveTowers(IEnumerable<CoordinatesValues> wallsCoordinates) =>
            wallsCoordinates
                .Where(coordinates => !CurrentDataService.FieldModel.GetCellData(coordinates).TowerIsConfirmed)
                .ForEach(coordinates => CurrentDataService.FieldModel.GetCellData(coordinates).RemoveTowerData());

        private void PlaceWalls(IEnumerable<CoordinatesValues> wallsCoordinates) =>
            wallsCoordinates
                .Where(coordinates => !CurrentDataService.FieldModel.GetCellData(coordinates).TowerIsConfirmed)
                .ForEach(coordinates => CurrentDataService.FieldModel.GetCellData(coordinates).SetWallData(GameFactory.FieldFactory.CreateWallData()));
    }
}