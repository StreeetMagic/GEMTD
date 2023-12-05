using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;

namespace Gameplay.Walls.WallPlacers
{
    [CreateAssetMenu(fileName = "WallPlacerConfig", menuName = "Configs/WallPlacerConfig")]
    public class WallPlacerConfig : ScriptableObject, IStaticData
    {
        public int SAVER;
        public int towerPerRound = 5;
        public List<WallSettingsPerRound> WallSettingsPerRounds = new();

        public void AddPlacedTower(CoordinatesValues coordinatesValues)
        {
            if (WallSettingsPerRounds.Count == 0)
            {
                WallSettingsPerRounds.Add(new WallSettingsPerRound(1337));
            }

            WallSettingsPerRound lastWallSettingsPerRound = WallSettingsPerRounds.LastOrDefault();

            if (lastWallSettingsPerRound != null && lastWallSettingsPerRound.PlaceCount >= towerPerRound)
            {
                WallSettingsPerRounds.Add(new WallSettingsPerRound(1337));
                lastWallSettingsPerRound = WallSettingsPerRounds.Last();
            }

            if (lastWallSettingsPerRound != null)
                lastWallSettingsPerRound.PlaceList.Add(coordinatesValues);
        }

        public void RemovePlacedTower(CoordinatesValues coordinatesValues)
        {
            var lastWallSettingsPerRound = WallSettingsPerRounds.LastOrDefault();

            if (lastWallSettingsPerRound != null && lastWallSettingsPerRound.PlaceCount >= towerPerRound)
            {
                WallSettingsPerRounds.Add(new WallSettingsPerRound(1337));
                lastWallSettingsPerRound = WallSettingsPerRounds.Last();
            }

            if (lastWallSettingsPerRound != null)
                lastWallSettingsPerRound.DestroyList.Add(coordinatesValues);
        }
    }
}