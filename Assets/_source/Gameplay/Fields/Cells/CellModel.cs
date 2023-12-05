using System;
using Gameplay.Blocks;
using Gameplay.Checkpoints;
using Gameplay.Towers;
using Gameplay.Walls;

namespace Gameplay.Fields.Cells
{
    public class CellModel
    {
        public CellModel(CoordinatesValues coordinatesValues, BlockModel blockModel)
        {
            CoordinatesValues = coordinatesValues;
            BlockModel = blockModel;
        }

        public event Action ChekpointDataSet;
        public event Action WallDataSet;
        public event Action WallDataRemoved;
        public event Action TowerDataSet;
        public event Action TowerDataRemoved;
        public event Action TowerConfirmed;

        public CoordinatesValues CoordinatesValues { get; }
        public CheckPointModel CheckPointModel { get; private set; }
        public WallData WallData { get; private set; }
        public BlockModel BlockModel { get; private set; }
        public TowerModel TowerModel { get; private set; }

        public bool IsEmpty => CheckPointModel == null && WallData == null && TowerModel == null;
        public bool HasWall => WallData != null;
        public bool CanBeReplacedWithTower => WallData != null && TowerModel == null && CheckPointModel == null && TowerIsConfirmed == false;
        public bool TowerIsConfirmed { get; set; }

        public void SetCheckpointData(CheckPointModel checkPointModel)
        {
            CheckPointModel = checkPointModel;
            ChekpointDataSet?.Invoke();
        }

        public void SetTowerData(TowerModel towerModel)
        {
            TowerModel = towerModel;
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
            TowerModel = null;
            TowerDataRemoved?.Invoke();
        }

        public void ConfirmTower()
        {
            TowerConfirmed?.Invoke();
            TowerIsConfirmed = true;
        }
    }
}