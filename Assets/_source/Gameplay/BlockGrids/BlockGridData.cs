using System.Linq;
using UnityEngine;

namespace Infrastructure.Services.GameFactoryServices
{
    public class BlockGridData
    {
        private readonly CellData[] _cellDatas;

        public BlockGridData(CellData[] cellDatas)
        {
            _cellDatas = cellDatas;
            Debug.Log("создали block grid data");
            Debug.Log(_cellDatas.Length + " ячеек");
        }

        public CellData[] CellDatas => _cellDatas.ToArray();
    }
}