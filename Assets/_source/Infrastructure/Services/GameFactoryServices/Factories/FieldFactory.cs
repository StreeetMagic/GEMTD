﻿using System.Linq;
using GameDesign;
using Gameplay.Fields;
using Gameplay.Fields.Blocks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.EnemySpawners;
using Gameplay.Fields.EnemySpawners.EnemyContainers;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Towers.Shooters;
using Gameplay.Fields.Towers.TargetDetectors;
using Gameplay.Fields.Walls;
using Games;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;
using Infrastructure.Services.CurrentDataServices;
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

        public FieldModel CreateFieldModel() =>
            new(CreateCellModels(_staticDataService.Get<FieldConfig>().FieldSize), CreateEnemySpawnerModel());

        private EnemySpawnerModel CreateEnemySpawnerModel() =>
            new(CreateEnemyContainerModel());

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
                .With(e => e.transform.localPosition = new Vector3(cellModel.CoordinatesValues.X, 0, cellModel.CoordinatesValues.Z))
                .With(e => e.name = "Cell (" + cellModel.CoordinatesValues.X + ", " + cellModel.CoordinatesValues.Z + ")");

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
                .With(e => e.Init(towerModel, _staticDataService.Get<TowerConfig>().TowerMaterials[towerModel.Type]))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = Vector3.zero);

        public void CreateCheckpointsDatas()
        {
            CheckpointValues[] configs = _staticDataService.Get<CheckpointsConfig>().CheckPointSettings;

            CheckPointModel[] checkpointDatas = new CheckPointModel[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                CellModel cell = _currentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(configs[i].CoordinatesValues);
                checkpointDatas[i] = CreateCheckPointData(configs[i].Number, cell);
                cell.SetCheckpointModel(checkpointDatas[i]);
            }
        }

        public WallModel CreateWallData() =>
            new WallModel();

        public void PaintBlocks() =>
            _staticDataService.Get<PaintedBlockConfig>().Coordinates.ForEach(coordinates => { _currentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinates).BlockModel.Paint(); });

        public TowerModel CreateTowerData(TowerType towerType, int level)
        {
            var singleProjectileShooterModel = new SingleProjectileShooterModel();
            return new TowerModel(towerType, level, singleProjectileShooterModel, new TargetDetetcorModel(singleProjectileShooterModel));
        }

        public void CreateStartingLabyrinth() =>
            _staticDataService.Get<StartingLabyrinthConfig>().Coordinates.ToList().ForEach(coordinate => _currentDataService.FieldModel.CellsContainerModel.GetCellModel(coordinate)
                .SetWallModel(CreateWallData()));

        private CheckPointModel CreateCheckPointData(int number, CellModel cell) =>
            new CheckPointModel(number, cell);

        private CellModel[] CreateCellModels(int size) =>
            Enumerable.Range(0, size)
                .SelectMany(i => Enumerable.Range(0, size)
                    .Select(j => new CellModel(new CoordinatesValues(i, j), new BlockModel())))
                .ToArray();
    }
}