using System;
using System.Linq;
using InfastuctureCore.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Fields.Checkpoints
{
    [CreateAssetMenu(menuName = "Configs/Checkpoints Config", fileName = "CheckpointsConfig")]
    public class CheckpointsConfig : ScriptableObject, IStaticData
    {
        [SerializeField] private CheckpointValues[] _checkPointSettings;

        public CheckpointValues[] CheckPointSettings => _checkPointSettings.ToArray();
    }

    [Serializable]
    public class CheckpointValues
    {
        [Range(0, 6)] public int Number;

        [FormerlySerializedAs("CoordinatesValues")]
        public Vector2Int Coordinates;
    }
}