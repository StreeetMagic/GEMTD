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

        private readonly BlockGridFactory _blockGridFactory;

        public GameFactoryService(IAssetProviderService assetProviderService, IStaticDataService staticDataService, ICurrentDataService currentDataService)
        {
            _assetProviderService = assetProviderService;
            _staticDataService = staticDataService;
            _currentDataService = currentDataService;

            _blockGridFactory = new BlockGridFactory(assetProviderService, staticDataService, currentDataService);
        }

        public void CreateBlockGrid()
        {
            _blockGridFactory.CreateBlockGrid();
        }
    }
}