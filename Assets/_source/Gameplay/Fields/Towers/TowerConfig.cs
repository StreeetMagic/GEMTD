using System.Collections.Generic;
using Gameplay.Fields.Towers.Resources;
using InfastuctureCore.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Fields.Towers
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Configs/TowerConfig")]
    public class TowerConfig : SerializedScriptableObject, IStaticData
    {
        public Dictionary<TowerType, Material> TowerMaterials = new();
    }
}