using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Towers.Resources;
using Gameplay.Fields.Walls;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.GameStateMachines;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Fields.WallPlacers
{
    public class TowerPlacer
    {
        private int _roundNumber = 1;
        private float _seconds = .001f;

        private WallPlacerConfig WallPlacerConfig => ServiceLocator.Instance.Get<IStaticDataService>().Get<WallPlacerConfig>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

        public IEnumerator PlaceTowers(Action onComplete)
        {
            Debug.Log("Round number " + _roundNumber);
            Debug.Log("Round with walls " + WallPlacerConfig.WallSettingsPerRounds.Count);

            int roundIndex = _roundNumber - 1;

            Coordinates[] wallsCoordinates = GetWallCoordinates();

            if (_roundNumber < WallPlacerConfig.WallSettingsPerRounds.Count)
            {
                if (WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList.Count > 0)
                {
                    foreach (Coordinates coordinates in WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList)
                    {
                        yield return new WaitForSeconds(_seconds);

                        RemoveWall(coordinates);
                    }
                }
            }

            for (int i = 0; i < wallsCoordinates.Length; i++)
            {
                yield return new WaitForSeconds(_seconds);

                AddTower(wallsCoordinates[i]);
            }

            ChooseRandomTower(wallsCoordinates);

            _roundNumber++;
            onComplete?.Invoke();
        }

        private Coordinates[] GetWallCoordinates()
        {
            if (_roundNumber >= WallPlacerConfig.WallSettingsPerRounds.Count)
            {
                Debug.Log("стены закончились");
                return CurrentDataService.FieldData.GetCentalWalls(WallPlacerConfig.towerPerRound);
            }
            else
            {
                return WallPlacerConfig.WallSettingsPerRounds[_roundNumber - 1].PlaceList.ToArray();
            }
        }

        private void AddTower(Coordinates coordinates)
        {
            TowerType towerType = (TowerType)Random.Range(0, 8);
            int level = 1;
            CurrentDataService.FieldData.GetCellData(coordinates).SetTowerData(GameFactory.BlockGridFactory.CreateTowerData(towerType, level));
        }

        private void AddWall(Coordinates coordinates)
        {
            CurrentDataService.FieldData.GetCellData(coordinates).SetWallData(GameFactory.BlockGridFactory.CreateWallData());
        }

        private void RemoveWall(Coordinates coordinates)
        {
            CurrentDataService.FieldData.GetCellData(coordinates).RemoveWallData();
        }

        private void ChooseRandomTower(Coordinates[] placedTowers)
        {
            int randomIndex = Random.Range(0, placedTowers.Length);

            for (int i = 0; i < placedTowers.Length; i++)
            {
                if (i == randomIndex)
                {
                    var cellData = CurrentDataService.FieldData.GetCellData(placedTowers[i]);
                    cellData.PlaceTower();
                    continue;
                }

                CurrentDataService.FieldData.GetCellData(placedTowers[i]).ReplaceTowerWithWall(GameFactory.BlockGridFactory.CreateWallData());
            }
        }
    }
}