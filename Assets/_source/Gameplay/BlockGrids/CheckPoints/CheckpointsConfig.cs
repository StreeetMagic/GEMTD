using System;
using System.Linq;
using Gameplay.BlockGrids.Cells;
using UnityEngine;

namespace Gameplay.BlockGrids.Checkpoints
{
    [CreateAssetMenu(menuName = "Configs/Checkpoints Config", fileName = "CheckpointsConfigSO")]
    public class CheckpointsConfig : ScriptableObject
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