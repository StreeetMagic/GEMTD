using Games;
using Games.Config.Resources;
using InfastuctureCore.Services;
using InfastuctureCore.Services.AssetProviderServices;
using Infrastructure.Services.CurrentDataServices;

namespace Infrastructure.Services.GameFactoryServices
{
    public interface IGameFactoryService : IService
    {
        void CreateBlockGrid();
    }

    class GameFactoryService : IGameFactoryService
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICurrentDataService _currentDataService;

        public GameFactoryService(IAssetProviderService assetProviderService, IStaticDataService staticDataService, ICurrentDataService currentDataService)
        {
            _assetProviderService = assetProviderService;
            _staticDataService = staticDataService;
            _currentDataService = currentDataService;
        }

        public void CreateBlockGrid()
        {
            BlockGridData blockGridData = _currentDataService.Register(CreateBlockGridData());
            _assetProviderService.Instantiate<BlockGridView>(Constants.AssetsPath.Prefabs.BlockGrid).Init(blockGridData);
        }

        private BlockGridData CreateBlockGridData()
        {
            int xSize = _staticDataService.Get<GameConfig>().FieldXSize;
            int ySize = _staticDataService.Get<GameConfig>().FieldYSize;

            CellData[] cellDatas = new CellData[xSize * ySize];

            int count = 0;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    count++;
                    cellDatas[count] = new CellData(new Coordinates(i, j));
                }
            }

            return new BlockGridData(cellDatas);
        }
    }
}