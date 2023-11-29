using System;
using Gameplay.Fields.Blocks;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Walls;
using UnityEngine;

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


        public Coordinates Coordinates { get; }
        public CheckpointData CheckpointData { get; private set; }
        public WallData WallData { get; private set; }
        public BlockData BlockData { get; private set; }


        public bool IsEmpty => CheckpointData == null && WallData == null;
        public bool HasWall => WallData != null;

        public void SetCheckpointData(CheckpointData checkpointData)
        {
            CheckpointData = checkpointData;
            ChekpointDataSet?.Invoke();
        }

        public void SetWallData(WallData wallData)
        {
            if (IsEmpty == false)
            {
                Debug.Log("Cell is not empty");
                return;
            }

            WallData = wallData;
            WallDataSet?.Invoke();
        }

        public void RemoveWallData()
        {
            WallData = null;
            WallDataRemoved?.Invoke();
        }


    }

    [Serializable]
    public struct Coordinates
    {
        public int X;
        public int Z;

        public Coordinates(int x, int z)
        {
            X = x;
            Z = z;
        }
    }
}