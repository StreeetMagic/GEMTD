namespace Infrastructure.Services.GameFactoryServices
{
    public class BlockGridData
    {
        private readonly CellData[] _cellDatas;

        public BlockGridData(CellData[] cellDatas)
        {
            _cellDatas = cellDatas;
        }
    }
}