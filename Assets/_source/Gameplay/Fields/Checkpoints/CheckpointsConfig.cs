using System;
using System.Linq;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;

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

        public CoordinatesValues CoordinatesValues;
    }
}