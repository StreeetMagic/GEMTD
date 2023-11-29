﻿using System.Linq;
using Gameplay.Fields.Cells;

namespace Gameplay.Fields
{
    public class FieldData
    {
        private readonly CellData[] _cellDatas;

        public FieldData(CellData[] cellDatas)
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