using System.Collections.Generic;
using Gameplay.Fields;
using InfastuctureCore.Services;
using UnityEngine;

namespace GameDesign
{
    [CreateAssetMenu(fileName = "PaintedBlockConfig", menuName = "Configs/PaintedBlockConfig")]
    public class PaintedBlockConfig : ScriptableObject, IStaticData
    {
        public List<Coordinates> Coordinates;
    }
}