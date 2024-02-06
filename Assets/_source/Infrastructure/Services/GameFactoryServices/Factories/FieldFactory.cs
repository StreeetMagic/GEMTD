using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields;
using Gameplay.Fields.Blocks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.EnemyContainers;
using Gameplay.Fields.EnemySpawners;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.Thrones;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Towers.Shooters;
using Gameplay.Fields.Towers.TargetDetectors;
using Gameplay.Fields.Walls;
using Games;
using Infrastructure.Services.AssetProviderServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.StaticDataServices;
using Infrastructure.Utilities;
using UnityEngine;

namespace Infrastructure.Services.GameFactoryServices.Factories
{
    public class FieldFactory
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICurrentDataService _currentDataService;

        public FieldFactory(IAssetProviderService assetProviderService, IStaticDataService staticDataService, ICurrentDataService currentDataService)
        {
            _assetProviderService = assetProviderService;
            _staticDataService = staticDataService;
            _currentDataService = currentDataService;
        }

        public FieldModel CreateFieldModel(GameConfig gameConfig) =>
            new(CreateCellModels(_staticDataService.Get<FieldConfig>().FieldSize), CreateEnemySpawnerModel(gameConfig));

        private EnemySpawnerModel CreateEnemySpawnerModel(GameConfig gameConfig) =>
            new(CreateEnemyContainerModel(), gameConfig.SpawnCooldown, gameConfig.WaveMobCount);

        private EnemyContainerModel CreateEnemyContainerModel() =>
            new();

        public BlockView CreateBlockView(BlockModel blockModel, Transform parent) =>
            _assetProviderService.Instantiate<BlockView>(Constants.AssetsPath.Prefabs.Block, Vector3.zero)
                .With(e => e.Init(blockModel))
                .With(e => e.transform.SetParent(parent))
                .With(e => e.transform.localPosition = Vector3.zero);

        public CheckpointView CreateCheckpointView(CheckPointModel checkPointModel, Transform transform) =>
            _assetProviderService.Instantiate<CheckpointView>(Constants.AssetsPath.Prefabs.Checkpoint, Vector3.zero)
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = Vector3.zero)
                .With(e => e.name = "Checkpoint " + checkPointModel.Number);

        public CellView CreateCellView(CellModel cellModel, Transform transform) =>
            _assetProviderService.Instantiate<CellView>(Constants.AssetsPath.Prefabs.Cell, Vector3.zero)
                .With(e => e.Init(cellModel))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = new Vector3(cellModel.Coordinates.x, 0, cellModel.Coordinates.y))
                .With(e => e.name = "Cell (" + cellModel.Coordinates.x + ", " + cellModel.Coordinates.y + ")");

        public FieldView CreateFieldView(FieldModel fieldModel) =>
            _assetProviderService.Instantiate<FieldView>(Constants.AssetsPath.Prefabs.Field)
                .With(e => e.Init(fieldModel));

        public WallView CreateWallView(WallModel wallModel, Transform transform) =>
            _assetProviderService.Instantiate<WallView>(Constants.AssetsPath.Prefabs.Wall, Vector3.zero)
                .With(e => e.Init(wallModel))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = Vector3.zero);

        public TowerView CreateTowerView(TowerModel towerModel, Transform transform) =>
            _assetProviderService.Instantiate<TowerView>(Constants.AssetsPath.Prefabs.Tower, Vector3.zero)
                .With(e => e.Init(towerModel, _staticDataService.Get<TowersConfig>().GetMaterial(towerModel.Type)))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = Vector3.zero)
                .With(e => e.SetScale(_staticDataService.Get<TowersConfig>().GetScale(towerModel.Level)));

        public void CreateCheckpointsModels()
        {
            IReadOnlyList<CheckpointValues> configs = _staticDataService.Get<CheckpointsConfig>().CheckpointValues;

            var checkpointDatas = new CheckPointModel[configs.Count];

            for (int i = 0; i < configs.Count; i++)
            {
                CellModel cell = _currentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(configs[i].Coordinates);
                checkpointDatas[i] = CreateCheckPointModel(configs[i].Number, cell);
                cell.SetCheckpointModel(checkpointDatas[i]);
            }
        }

        public WallModel CreateWallModel() =>
            new WallModel();

        public TowerModel CreateTowerModel(TowerType towerType, int level)
        {
            var singleProjectileShooterModel = new SingleProjectileShooterModel();
            return new TowerModel(towerType, singleProjectileShooterModel, new TargetDetetcorModel(singleProjectileShooterModel), level);
        }

        public void CreateStartingLabyrinth() =>
            _staticDataService.Get<StartingLabyrinthConfig>().Coordinates.ToList().ForEach(coordinate => _currentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinate)
                .SetWallModel(CreateWallModel()));

        private CheckPointModel CreateCheckPointModel(int number, CellModel cell) =>
            new CheckPointModel(number, cell);

        private CellModel[] CreateCellModels(int size) =>
            Enumerable.Range(0, size)
                .SelectMany(i => Enumerable.Range(0, size)
                    .Select(j => new CellModel(new Vector2Int(i, j), new BlockModel())))
                .ToArray();

        public ThroneModel CreateThroneModel() =>
            new(_staticDataService.Get<GameConfig>().ThroneHealth);
    }
}