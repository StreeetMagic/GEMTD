using System;
using System.Collections.Generic;
using InfastuctureCore.Services.StaticDataServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Fields.Towers
{
    [CreateAssetMenu(fileName = "TowersConfig", menuName = "Configs/TowersConfig")]
    public class TowersConfig : SerializedScriptableObject, IStaticData
    {
        public Dictionary<TowerType, TowerValues> TowerValues = new();
    }

    [Serializable]
    public class TowerValues
    {
        public Material Material;
    }
}