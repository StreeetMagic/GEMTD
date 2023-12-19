using Gameplay.Fields.EnemySpawners.Enemies;
using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles;
using Games;
using InfastuctureCore.Services;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Utilities;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices.Factories;
using UnityEngine;
using UserInterface;
using IStaticDataService = InfastuctureCore.Services.StaticDataServices.IStaticDataService;

namespace Infrastructure.Services.GameFactoryServices
{
    public interface IGameFactoryService : IService
    {
        FieldFactory FieldFactory { get; }

        UserInterfaceFactory UserInterfaceFactory { get; }
        
        void CreateProjectile(Transform shootingPoint, EnemyModel target);

        EnemyModel CreateEnemyModel(Vector3 at, Vector2Int[] points);

        EnemyView CreateEnemyView(Vector3 position, EnemyModel model);
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
            UserInterfaceFactory = new UserInterfaceFactory(_assetProvider);
            // _staticData = staticData;
            // _currentData = currentData;
        }

        public FieldFactory FieldFactory { get; }
        public UserInterfaceFactory UserInterfaceFactory { get; }

        public void CreateProjectile(Transform shootingPoint, EnemyModel target) =>
            _assetProvider.Instantiate<DefaultProjectileView>(Constants.AssetsPath.Prefabs.Projectile, shootingPoint.position)
                .With(e => e.Init(new DefaultProjectileModel(target)));

        public EnemyModel CreateEnemyModel(Vector3 at, Vector2Int[] points) =>
            new EnemyModel(at, points);

        public EnemyView CreateEnemyView(Vector3 position, EnemyModel model) =>
            _assetProvider.Instantiate<EnemyView>(Constants.AssetsPath.Prefabs.Enemy, position)
                .With(e => e.Init(model));
    }

    public class UserInterfaceFactory
    {
        private IAssetProviderService _assetProvider;

        public UserInterfaceFactory(IAssetProviderService assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public HeadUpDisplay CreateHeadUpDisplay() =>
            _assetProvider.Instantiate<HeadUpDisplay>(Constants.AssetsPath.Prefabs.HeadUpDisplay);
    }
}