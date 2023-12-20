using System.Collections.Generic;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields.Walls.WallPlacers
{
    [CreateAssetMenu(fileName = "WallPlacerConfig", menuName = "Configs/WallPlacerConfig")]
    public class WallPlacerConfig : ScriptableObject, IStaticData
    {
        public int towerPerRound = 5;
        public List<WallSettingsPerRound> WallSettingsPerRounds = new();
    }
}