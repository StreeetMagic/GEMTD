using System;
using System.Linq;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Checkpoints
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

        [FormerlySerializedAs("Coordinates")] public CoordinatesValues _coordinatesValues;
    }
}