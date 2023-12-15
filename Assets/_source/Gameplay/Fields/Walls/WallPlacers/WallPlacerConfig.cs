using System.Collections.Generic;
using System.Linq;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields.Walls.WallPlacers
{
    [CreateAssetMenu(fileName = "WallPlacerConfig", menuName = "Configs/WallPlacerConfig")]
    public class WallPlacerConfig : ScriptableObject, IStaticData
    {
        public int towerPerRound = 5;
        public List<WallSettingsPerRound> WallSettingsPerRounds = new();

        public void AddPlacedTower(Vector2Int coordinatesValues)
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

        public void RemovePlacedTower(Vector2Int coordinatesValues)
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