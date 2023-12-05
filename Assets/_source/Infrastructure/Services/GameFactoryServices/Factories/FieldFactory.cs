using System.Linq;
using GameDesign;
using Gameplay.Blocks;
using Gameplay.Checkpoints;
using Gameplay.Fields;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Labytinths;
using Gameplay.Towers;
using Gameplay.Towers.Shooters;
using Gameplay.Towers.TargetDetectors;
using Gameplay.Walls;
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

        public FieldModel CreateFieldData() =>
            new(CreateCellDatas(_staticDataService.Get<FieldConfig>().FieldSize));

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

        public FieldView CreateBlockGridView(FieldModel fieldModel) =>
            _assetProviderService.Instantiate<FieldView>(Constants.AssetsPath.Prefabs.Field)
                .With(e => e.Init(fieldModel));

        public WallView CreateWallView(WallData wallData, Transform transform) =>
            _assetProviderService.Instantiate<WallView>(Constants.AssetsPath.Prefabs.Wall, Vector3.zero)
                .With(e => e.Init(wallData))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = Vector3.zero);

        public TowerView CreateTowerView(TowerModel towerModel, Transform transform) =>
            _assetProviderService.Instantiate<TowerView>(Constants.AssetsPath.Prefabs.Tower, Vector3.zero)
                .With(e => e.Init(towerModel, _staticDataService.Get<TowerConfig>().TowerMaterials[towerModel.Type]))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = Vector3.zero);

        public void CreateCheckpointsDatas()
        {
            CheckpointSettings[] configs = _staticDataService.Get<CheckpointsConfig>().CheckPointSettings;

            CheckPointModel[] checkpointDatas = new CheckPointModel[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                checkpointDatas[i] = CreateCheckPointData(configs[i].Number);
                CellModel cell = GetCellDataByCoordinates(configs[i]._coordinatesValues);
                cell.SetCheckpointData(checkpointDatas[i]);
            }
        }

        public WallData CreateWallData() =>
            new WallData();

        public void PaintBlocks() =>
            _staticDataService.Get<PaintedBlockConfig>().Coordinates.ForEach(coordinates => { _currentDataService.FieldModel.GetCellData(coordinates).BlockModel.Paint(); });

        public TowerModel CreateTowerData(TowerType towerType, int level)
        {
            var singleProjectileShooterModel = new SingleProjectileShooterModel();
            return new TowerModel(towerType, level, singleProjectileShooterModel, new TargetDetetcorModel(singleProjectileShooterModel));
        }

        public void CreateStartingLabyrinth() =>
            _staticDataService.Get<StartingLabyrinthConfig>().Coordinates.ToList().ForEach(coordinate => _currentDataService.FieldModel.GetCellData(coordinate)
                .SetWallData(CreateWallData()));

        private CheckPointModel CreateCheckPointData(int number) =>
            new CheckPointModel(number);

        private CellModel[] CreateCellDatas(int size) =>
            Enumerable.Range(0, size)
                .SelectMany(i => Enumerable.Range(0, size)
                    .Select(j => new CellModel(new CoordinatesValues(i, j), new BlockModel())))
                .ToArray();

        private CellModel GetCellDataByCoordinates(CoordinatesValues coordinatesValues) =>
            _currentDataService.FieldModel.CellDatas.FirstOrDefault(cellData => cellData.CoordinatesValues.X == coordinatesValues.X && cellData.CoordinatesValues.Z == coordinatesValues.Z);
    }
}