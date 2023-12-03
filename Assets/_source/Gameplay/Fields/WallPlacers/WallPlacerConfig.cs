using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;

namespace Gameplay.Fields.WallPlacers
{
    [CreateAssetMenu(fileName = "WallPlacerConfig", menuName = "Configs/WallPlacerConfigSO")]
    public class WallPlacerConfig : ScriptableObject, IStaticData
    {
        public int towerPerRound = 5;
        public List<WallSettingsPerRound> WallSettingsPerRounds = new();

        public void AddPlacedTower(Coordinates coordinates)
        {
            if (WallSettingsPerRounds.Count == 0)
            {
                WallSettingsPerRounds.Add(new WallSettingsPerRound());
            }

            WallSettingsPerRound lastWallSettingsPerRound = WallSettingsPerRounds.LastOrDefault();

            if (lastWallSettingsPerRound.PlaceCount >= towerPerRound)
            {
                WallSettingsPerRounds.Add(new WallSettingsPerRound());
                lastWallSettingsPerRound = WallSettingsPerRounds.Last();
            }
            
            lastWallSettingsPerRound.PlaceList.Add(coordinates);
        }

        public void RemovePlacedTower(Coordinates coordinates)
        {
            var lastWallSettingsPerRound = WallSettingsPerRounds.LastOrDefault();

            if (lastWallSettingsPerRound.PlaceCount >= towerPerRound)
            {
                WallSettingsPerRounds.Add(new WallSettingsPerRound());
                lastWallSettingsPerRound = WallSettingsPerRounds.Last();
            }

            lastWallSettingsPerRound.DestroyList.Add(coordinates);
        }

        [Serializable]
        public class WallSettingsPerRound
        {
            public int RoundNumber;
            public List<Coordinates> DestroyList = new List<Coordinates>();
            public List<Coordinates> PlaceList = new List<Coordinates>();
            public int PlaceCount => PlaceList.Count;
        }
    }
}