using System;
using Gameplay.Fields.Blocks;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Towers.Resources;
using Gameplay.Fields.Walls;

namespace Gameplay.Fields.Cells
{
    public class CellData
    {
        public CellData(Coordinates coordinates, BlockData blockData)
        {
            Coordinates = coordinates;
            BlockData = blockData;
        }

        public event Action ChekpointDataSet;
        public event Action WallDataSet;
        public event Action WallDataRemoved;
        public event Action TowerDataSet;
        public event Action TowerDataRemoved;
        public event Action TowerConfirmed;

        public Coordinates Coordinates { get; }
        public CheckpointData CheckpointData { get; private set; }
        public WallData WallData { get; private set; }
        public BlockData BlockData { get; private set; }
        public TowerData TowerData { get; private set; }

        public bool IsEmpty => CheckpointData == null && WallData == null && TowerData == null;
        public bool HasWall => WallData != null;
        public bool CanBeReplacedWithTower => WallData != null && TowerData == null && CheckpointData == null && TowerIsConfirmed == false;
        public bool TowerIsConfirmed { get; set; }

        public void SetCheckpointData(CheckpointData checkpointData)
        {
            CheckpointData = checkpointData;
            ChekpointDataSet?.Invoke();
        }

        public void SetTowerData(TowerData towerData)
        {
            TowerData = towerData;
            TowerDataSet?.Invoke();
        }

        public void SetWallData(WallData wallData)
        {
            WallData = wallData;
            WallDataSet?.Invoke();
        }

        public void RemoveWallData()
        {
            WallData = null;
            WallDataRemoved?.Invoke();
        }

        public void RemoveTowerData()
        {
            TowerData = null;
            TowerDataRemoved?.Invoke();
        }

        public void ConfirmTower()
        {
            TowerConfirmed?.Invoke();
            TowerIsConfirmed = true;
        }
    }
}