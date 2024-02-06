using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Towers;
using Infrastructure;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using Infrastructure.Services.StaticDataServices;
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

        public async UniTask PlaceTowers(List<TowerType> towerTypes, List<int> levels)
        {
            List<Vector2Int> wallsCoordinates = GetWallCoordinates();
            RemovePlacedWalls(CurrentDataService.FieldModel.RoundNumber - 1);

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
            CurrentDataService.FieldModel.RoundNumber <= WallPlacerConfig.WallSettingsPerRounds.Count
                ? WallPlacerConfig.WallSettingsPerRounds[CurrentDataService.FieldModel.RoundNumber - 1].PlaceList
                : CurrentDataService.FieldModel.GetCentralWalls(WallPlacerConfig.TowerPerRound).ToList();

        private void RemovePlacedWalls(int roundIndex)
        {
            if (CurrentDataService.FieldModel.RoundNumber >= WallPlacerConfig.WallSettingsPerRounds.Count)
                return;

            if (WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList.Count <= 0)
                return;

            foreach (Vector2Int coordinates in WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList)
                CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).RemoveWallModel();
        }

        private async UniTask SetTowers(List<Vector2Int> wallsCoordinates, List<TowerType> towerTypes, List<int> levels)
        {
            for (int i = 0; i < wallsCoordinates.ToList().Count; i++)
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
            CellModel cellModel = CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues);

            if (cellModel.WallModel != null)
                cellModel.RemoveWallModel();

            cellModel.SetTowerModel(GameFactory.FieldFactory.CreateTowerModel(towerType, level));
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