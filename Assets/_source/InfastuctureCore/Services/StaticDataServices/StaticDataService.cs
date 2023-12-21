using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Utilities;
using UnityEngine;

namespace InfastuctureCore.Services.StaticDataServices
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProviderService _assetProviderService;

        public StaticDataService(IAssetProviderService assetProviderService) =>
            _assetProviderService = assetProviderService;

        public void Register<TStaticData>() where TStaticData : Object, IStaticData =>
            Implementation<TStaticData>.Instance = _assetProviderService.Get<TStaticData>();

        public void Register<TStaticData>(TStaticData staticData) where TStaticData : IStaticData =>
            Implementation<TStaticData>.Instance = staticData;

        public TStaticData Get<TStaticData>() where TStaticData : IStaticData =>
            Implementation<TStaticData>.Instance;
    }
}