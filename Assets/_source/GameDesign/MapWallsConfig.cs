using System.Collections.Generic;
using Gameplay.Fields;
using InfastuctureCore.Services;
using UnityEngine;

namespace GameDesign
{
    [CreateAssetMenu(menuName = "Configs/MapWallsConfig", fileName = "MapWallsConfig")]
    public class MapWallsConfig : ScriptableObject, IStaticData
    {
        public List<Coordinates> Coordinates;
    }
}