using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Cells;
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
            await SetTowers(wallsCoordinates);
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

        private async UniTask SetTowers(List<Vector2Int> wallsCoordinates)
        {
            foreach (Vector2Int vector2Int in wallsCoordinates.ToList())
            {
                SetTower(vector2Int);
                await UniTask.Delay(_wallPlacementDelay);
            }
        }

        private void SetTower(Vector2Int coordinatesValues)
        {
            if (CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues).WallModel != null)
                CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues).RemoveWallModel();

            CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinatesValues).SetTowerModel(GameFactory.FieldFactory.CreateTowerModel((TowerType)Random.Range(0, 8), 1));
        }

        private async UniTask ReplaceTowersWithWalls(IEnumerable<Vector2Int> wallsCoordinates)
        {
            foreach (Vector2Int coordinates in wallsCoordinates)
            {
                if (CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).TowerIsConfirmed == false)
                {
                    CurrentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).RemoveTowerModel();
                }

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