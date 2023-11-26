using InfastuctureCore.Services;
using InfastuctureCore.Services.AssetProviderServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices.Factories;

namespace Infrastructure.Services.GameFactoryServices
{
    public interface IGameFactoryService : IService
    {
        BlockGridFactory BlockGridFactory { get; }
        LabyrinthFactory LabyrinthFactory { get; }
    }

    public class GameFactoryService : IGameFactoryService
    {
        public GameFactoryService(IAssetProviderService assetProviderService, IStaticDataService staticDataService, ICurrentDataService currentDataService)
        {
            BlockGridFactory = new BlockGridFactory(assetProviderService, staticDataService, currentDataService);
            LabyrinthFactory = new LabyrinthFactory(assetProviderService, staticDataService, currentDataService, BlockGridFactory);
        }

        public BlockGridFactory BlockGridFactory { get; }
        public LabyrinthFactory LabyrinthFactory { get; }
    }
}