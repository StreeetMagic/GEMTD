using System;
using System.Collections;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Towers.Resources;
using Gameplay.Fields.Walls;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.GameStateMachines;
using Infrastructure.Services.CurrentDataServices;
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

        public IEnumerator PlaceTowers(Coordinates[] placedTowers, Action<bool> onComplete)
        {
            Debug.Log("Round number " + _roundNumber);
            Debug.Log("Round with walls " + WallPlacerConfig.WallSettingsPerRounds.Count);

            if (_roundNumber >= WallPlacerConfig.WallSettingsPerRounds.Count)
            {
                Debug.Log("стены закончились");
                _roundNumber++;
                onComplete?.Invoke(false);
                yield return null;
            }
            else
            {
                int roundIndex = _roundNumber - 1;

                if (WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList.Count > 0)
                {
                    foreach (Coordinates coordinates in WallPlacerConfig.WallSettingsPerRounds[roundIndex].DestroyList)
                    {
                        yield return new WaitForSeconds(_seconds);

                        RemoveWall(coordinates);
                    }
                }

                for (int i = 0; i < WallPlacerConfig.WallSettingsPerRounds[roundIndex].PlaceList.Count; i++)
                {
                    Coordinates coordinates = WallPlacerConfig.WallSettingsPerRounds[roundIndex].PlaceList[i];
                    yield return new WaitForSeconds(_seconds);

                    AddTower(coordinates);
                    placedTowers[i] = coordinates;
                }

                _roundNumber++;
                onComplete?.Invoke(true);
            }
        }

        private void AddTower(Coordinates coordinates)
        {
            TowerType towerType = (TowerType)Random.Range(0, 8);
            int level = 1;
            CurrentDataService.FieldData.GetCellData(coordinates).SetTowerData(new TowerData(towerType, level));
        }

        private void AddWall(Coordinates coordinates)
        {
            CurrentDataService.FieldData.GetCellData(coordinates).SetWallData(new WallData());
        }

        private void RemoveWall(Coordinates coordinates)
        {
            CurrentDataService.FieldData.GetCellData(coordinates).RemoveWallData();
        }
    }
}