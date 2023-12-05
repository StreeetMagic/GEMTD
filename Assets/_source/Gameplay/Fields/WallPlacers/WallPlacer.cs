using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Towers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using Sirenix.Utilities;
using Random = UnityEngine.Random;

namespace Gameplay.Fields.WallPlacers
{
    public class TowerPlacer
    {
        private WallPlacerConfig WallPlacerConfig => ServiceLocator.Instance.Get<IStaticDataService>().Get<WallPlacerConfig>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void PlaceTowers(Action onComplete)
        {
            List<Coordinates> wallsCoordinates = GetWallCoordinates();

            PlaceNewWalls(CurrentDataService.FieldData.RoundNumber - 1);
            SetTowers(wallsCoordinates);
            ConfirmRandomTower(wallsCoordinates);
            RemoveTowers(wallsCoordinates);
            PlaceWalls(wallsCoordinates);

            onComplete?.Invoke();
        }

        private List<Coordinates> GetWallCoordinates() =>
            CurrentDataService.FieldData.RoundNumber < WallPlacerConfig.WallSettingsPerRounds.Count
                ? WallPlacerConfig.WallSettingsPerRounds[CurrentDataService.FieldData.RoundNumber - 1].PlaceList
                : CurrentDataService.FieldData.GetCentralWalls(WallPlacerConfig.towerPerRound).ToList();

        private void PlaceNewWalls(int roundIndex)
        {
            if (CurrentDataService.FieldData.RoundNumber >= WallPlacerConfig.WallSettingsPerRounds.Count)
                return;

            if (WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList.Count <= 0)
                return;

            foreach (Coordinates coordinates in WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList)
                CurrentDataService.FieldData.GetCellData(coordinates).RemoveWallData();
        }

        private void SetTowers(List<Coordinates> wallsCoordinates) =>
            wallsCoordinates.ToList().ForEach(SetTower);

        private void SetTower(Coordinates coordinates)
        {
            if (CurrentDataService.FieldData.GetCellData(coordinates).WallData != null)
                CurrentDataService.FieldData.GetCellData(coordinates).RemoveWallData();

            CurrentDataService.FieldData.GetCellData(coordinates).SetTowerData(GameFactory.FieldFactory.CreateTowerData((TowerType)Random.Range(0, 8), 1));
        }

        private void ConfirmRandomTower(IReadOnlyList<Coordinates> wallsCoordinates) =>
            CurrentDataService.FieldData.GetCellData(wallsCoordinates[Random.Range(0, wallsCoordinates.Count)]).ConfirmTower();

        private void RemoveTowers(IEnumerable<Coordinates> wallsCoordinates) =>
            wallsCoordinates
                .Where(coordinates => !CurrentDataService.FieldData.GetCellData(coordinates).TowerIsConfirmed)
                .ForEach(coordinates => CurrentDataService.FieldData.GetCellData(coordinates).RemoveTowerData());

        private void PlaceWalls(IEnumerable<Coordinates> wallsCoordinates) =>
            wallsCoordinates
                .Where(coordinates => !CurrentDataService.FieldData.GetCellData(coordinates).TowerIsConfirmed)
                .ForEach(coordinates => CurrentDataService.FieldData.GetCellData(coordinates).SetWallData(GameFactory.FieldFactory.CreateWallData()));
    }
}