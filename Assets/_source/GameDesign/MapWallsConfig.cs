using System.Collections.Generic;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace GameDesign
{
    [CreateAssetMenu(menuName = "Configs/MapWallsConfig", fileName = "MapWallsConfig")]
    public class MapWallsConfig : ScriptableObject, IStaticData
    {
        public List<Vector2Int> Coordinates;
    }
}