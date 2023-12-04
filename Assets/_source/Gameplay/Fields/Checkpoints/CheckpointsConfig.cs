using System;
using System.Linq;
using InfastuctureCore.Services;
using UnityEngine;

namespace Gameplay.Fields.Checkpoints
{
    [CreateAssetMenu(menuName = "Configs/Checkpoints Config", fileName = "CheckpointsConfig")]
    public class CheckpointsConfig : ScriptableObject, IStaticData
    {
        [SerializeField] private CheckpointSettings[] _checkPointSettings;

        public CheckpointSettings[] CheckPointSettings => _checkPointSettings.ToArray();
    }

    [Serializable]
    public class CheckpointSettings
    {
        [Range(0, 6)] public int Number;

        public Coordinates Coordinates;
    }
}