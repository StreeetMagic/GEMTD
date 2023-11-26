using UnityEngine;

namespace Gameplay.BlockGrids.Checkpoints
{
    public class CheckpointView : MonoBehaviour
    {
        private CheckpointData _checkpointData;

        public void Init(CheckpointData checkpointData)
        {
            _checkpointData = checkpointData;
        }
    }
}
