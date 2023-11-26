using System.Linq;
using Gameplay.BlockGrids.Cells;
using Gameplay.BlockGrids.Checkpoints;

namespace Gameplay.BlockGrids
{
    public class BlockGridData
    {
        private readonly CellData[] _cellDatas;

        public BlockGridData(CellData[] cellDatas)
        {
            _cellDatas = cellDatas;
        }

        public CellData[] CellDatas => _cellDatas.ToArray();

        public CellData GetCellDataByCoordinates(Coordinates coordinate)
        {
            foreach (CellData cellData in _cellDatas)
            {
                if (cellData.Coordinates.Equals(coordinate))
                {
                    return cellData;
                }
            }

            return null;
        }
    }
}