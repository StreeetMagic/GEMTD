using System.Collections.Generic;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace GameDesign
{
    [CreateAssetMenu(fileName = "PaintedBlockConfig", menuName = "Configs/PaintedBlockConfig")]
    public class PaintedBlockConfig : ScriptableObject, IStaticData
    {
        public List<Vector2Int> Coordinates;
    }
}