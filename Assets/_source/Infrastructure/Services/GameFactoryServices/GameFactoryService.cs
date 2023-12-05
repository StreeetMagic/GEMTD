using InfastuctureCore.Services;
using InfastuctureCore.Services.AssetProviderServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices.Factories;
using IStaticDataService = InfastuctureCore.Services.StaticDataServices.IStaticDataService;

namespace Infrastructure.Services.GameFactoryServices
{
    public interface IGameFactoryService : IService
    {
        FieldFactory FieldFactory { get; }
    }

    public class GameFactoryService : IGameFactoryService
    {
        public GameFactoryService(IAssetProviderService assetProvider, IStaticDataService staticData, ICurrentDataService currentData)
        {
            FieldFactory = new FieldFactory(assetProvider, staticData, currentData);
        }

        public FieldFactory FieldFactory { get; }
    }
}