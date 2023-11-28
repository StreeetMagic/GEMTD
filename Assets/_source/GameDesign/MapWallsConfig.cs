using System.Collections.Generic;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;

namespace GameDesign
{
    [CreateAssetMenu(menuName = "Configs/MapWallsConfig", fileName = "MapWallsConfigSO")]
    public class MapWallsConfig : ScriptableObject, IStaticData
    {
        public List<Coordinates> Coordinates;
    }
}