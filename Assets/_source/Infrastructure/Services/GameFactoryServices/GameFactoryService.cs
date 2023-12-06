using Gameplay.Towers.Shooters.Projectiles.DefaultProjectiles;
using Games;
using InfastuctureCore.Services;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Utilities;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices.Factories;
using UnityEngine;
using IStaticDataService = InfastuctureCore.Services.StaticDataServices.IStaticDataService;

namespace Infrastructure.Services.GameFactoryServices
{
    public interface IGameFactoryService : IService
    {
        FieldFactory FieldFactory { get; }
        void CreateProjectile(Transform shootingPoint, Transform target);
    }

    public class GameFactoryService : IGameFactoryService
    {
        private IAssetProviderService _assetProvider;
        // private IStaticDataService _staticData;
        // private ICurrentDataService _currentData;

        public GameFactoryService(IAssetProviderService assetProvider, IStaticDataService staticData, ICurrentDataService currentData)
        {
            FieldFactory = new FieldFactory(assetProvider, staticData, currentData);
            _assetProvider = assetProvider;
            // _staticData = staticData;
            // _currentData = currentData;
        }

        public FieldFactory FieldFactory { get; }

        public void CreateProjectile(Transform shootingPoint, Transform target) =>
            _assetProvider.Instantiate<DefaultProjectileView>(Constants.AssetsPath.Prefabs.Projectile, shootingPoint.position)
                .With(e => e.Init(new DefaultProjectileModel(target), target));
    }
}