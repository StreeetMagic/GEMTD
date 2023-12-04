using System.Collections.Generic;
using System.Linq;
using InfastuctureCore.Services;
using UnityEngine;

namespace Gameplay.Fields.WallPlacers
{
    [CreateAssetMenu(fileName = "WallPlacerConfig", menuName = "Configs/WallPlacerConfig")]
    public class WallPlacerConfig : ScriptableObject, IStaticData
    {
        public int SAVER;
        public int towerPerRound = 5;
        public List<WallSettingsPerRound> WallSettingsPerRounds = new();

        public void AddPlacedTower(Coordinates coordinates)
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
                lastWallSettingsPerRound.PlaceList.Add(coordinates);
        }

        public void RemovePlacedTower(Coordinates coordinates)
        {
            var lastWallSettingsPerRound = WallSettingsPerRounds.LastOrDefault();

            if (lastWallSettingsPerRound != null && lastWallSettingsPerRound.PlaceCount >= towerPerRound)
            {
                WallSettingsPerRounds.Add(new WallSettingsPerRound(1337));
                lastWallSettingsPerRound = WallSettingsPerRounds.Last();
            }

            if (lastWallSettingsPerRound != null)
                lastWallSettingsPerRound.DestroyList.Add(coordinates);
        }
    }
}