using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.CellsContainers;
using Gameplay.Fields.Towers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Fields.Walls.WallPlacers
{
    public class TowerPlacer
    {
        private readonly int _wallPlacementDelay = 100;

        private WallPlacerConfig WallPlacerConfig => ServiceLocator.Instance.Get<IStaticDataService>().Get<WallPlacerConfig>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

        public async UniTask PlaceTowers()
        {
            List<Vector2Int> wallsCoordinates = GetWallCoordinates();
            RemovePlacedWalls(CurrentDataService.FieldModel.RoundNumber - 1);
            await SetTowers(wallsCoordinates, new TowerType[] { TowerType.B1, TowerType.B2, TowerType.B3, TowerType.B4, TowerType.B5 });
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
            CurrentDataService.FieldModel.RoundNumber <= WallPlacerConfig.WallSettingsPerRounds.Count
                ? WallPlacerConfig.WallSettingsPerRounds[CurrentDataService.FieldModel.RoundNumber - 1].PlaceList
                : CurrentDataService.FieldModel.GetCentralWalls(WallPlacerConfig.towerPerRound).ToList();

        private void RemovePlacedWalls(int roundIndex)
        {
            if (CurrentDataService.FieldModel.RoundNumber >= WallPlacerConfig.WallSettingsPerRounds.Count)
                return;

            if (WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList.Count <= 0)
                return;

            foreach (Vector2Int coordinates in WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList)
                CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).RemoveWallModel();
        }

        private async UniTask SetTowers(List<Vector2Int> wallsCoordinates, TowerType[] towerTypes)
        {
            for (int i = 0; i < wallsCoordinates.ToList().Count; i++)
            {
                Vector2Int vector2Int = wallsCoordinates.ToList()[i];
                SetTower(vector2Int, towerTypes[i]);
                await UniTask.Delay(_wallPlacementDelay);
            }
        }

        private void SetTower(Vector2Int coordinatesValues, TowerType towerType)
        {
            CellModel cellModel = CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues);

            if (cellModel.WallModel != null)
                cellModel.RemoveWallModel();

            cellModel.SetTowerModel(GameFactory.FieldFactory.CreateTowerModel(towerType));
        }

        private async UniTask ReplaceTowersWithWalls(IEnumerable<Vector2Int> wallsCoordinates)
        {
            foreach (Vector2Int coordinates in wallsCoordinates)
            {
                CellModel cellModel = CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates);

                if (cellModel.TowerIsConfirmed == false)
                    cellModel.RemoveTowerModel();

                await UniTask.Delay(_wallPlacementDelay);

                await PlaceWalls(coordinates);
            }
        }

        private async UniTask PlaceWalls(Vector2Int coordinates)
        {
            CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).SetWallModel(GameFactory.FieldFactory.CreateWallModel());

            await UniTask.Delay(_wallPlacementDelay);
        }
    }
}