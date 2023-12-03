using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields;
using Gameplay.Fields.Blocks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Walls;
using Games;
using InfastuctureCore.Services.AssetProviderServices;
using InfastuctureCore.Utilities;
using Infrastructure.Services.CurrentDataServices;
using UnityEngine;
using IStaticDataService = InfastuctureCore.Services.StaticDataServices.IStaticDataService;

namespace Infrastructure.Services.GameFactoryServices.Factories
{
    public class BlockGridFactory
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICurrentDataService _currentDataService;

        public BlockGridFactory(IAssetProviderService assetProviderService, IStaticDataService staticDataService, ICurrentDataService currentDataService)
        {
            _assetProviderService = assetProviderService;
            _staticDataService = staticDataService;
            _currentDataService = currentDataService;
        }

        public FieldData CreateFieldData() =>
            new(CreateCellDatas());

        private CellData[] CreateCellDatas() =>
            CreateCellDatas(_staticDataService.Get<FieldConfig>().FieldXSize, _staticDataService.Get<FieldConfig>().FieldYSize);

        public BlockView CreateBlockView(BlockData blockData, Transform parent) =>
            _assetProviderService.Instantiate<BlockView>(Constants.AssetsPath.Prefabs.Block, Vector3.zero)
                .With(e => e.Init(blockData))
                .With(e => e.transform.SetParent(parent))
                .With(e => e.transform.localPosition = Vector3.zero);

        public CheckpointView CreateCheckpointView(CheckpointData checkpointData, Transform transform) =>
            _assetProviderService.Instantiate<CheckpointView>(Constants.AssetsPath.Prefabs.Checkpoint, Vector3.zero)
                .With(e => e.Init(checkpointData))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = Vector3.zero)
                .With(e => e.name = "Checkpoint " + checkpointData.Number);

        public CellView CreateCellView(CellData cellData, Transform transform) =>
            _assetProviderService.Instantiate<CellView>(Constants.AssetsPath.Prefabs.Cell, Vector3.zero)
                .With(e => e.Init(cellData))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = new Vector3(cellData.Coordinates.X, 0, cellData.Coordinates.Z))
                .With(e => e.name = "Cell (" + cellData.Coordinates.X + ", " + cellData.Coordinates.Z + ")");

        public FieldView CreateBlockGridView(FieldData fieldData) =>
            _assetProviderService.Instantiate<FieldView>(Constants.AssetsPath.Prefabs.Field)
                .With(e => e.Init(fieldData));

        public void CreateCheckpointsDatas()
        {
            CheckpointSettings[] configs = _staticDataService.Get<CheckpointsConfig>().CheckPointSettings;

            CheckpointData[] checkpointDatas = new CheckpointData[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                checkpointDatas[i] = CreateCheckPointData(configs[i].Number);
                CellData cell = GetCellDataByCoordinates(configs[i].Coordinates);
                cell.SetCheckpointData(checkpointDatas[i]);
            }
        }

        private CheckpointData CreateCheckPointData(int number) =>
            new(number);

        private CellData[] CreateCellDatas(int xSize, int ySize)
        {
            CellData[] cellDatas = new CellData[xSize * ySize];

            int count = 0;

            for (int i = 0; i < xSize; i++)
            for (int j = 0; j < ySize; j++)
                cellDatas[count++] = new CellData(new Coordinates(i, j), new BlockData());

            return cellDatas;
        }

        private CellData GetCellDataByCoordinates(Coordinates coordinates) =>
            _currentDataService.FieldData.CellDatas.FirstOrDefault(cellData => cellData.Coordinates.X == coordinates.X && cellData.Coordinates.Z == coordinates.Z);

        public WallView CreateWallView(WallData wallData, Transform transform) =>
            _assetProviderService.Instantiate<WallView>(Constants.AssetsPath.Prefabs.Wall, Vector3.zero)
                .With(e => e.Init(wallData))
                .With(e => e.transform.SetParent(transform))
                .With(e => e.transform.localPosition = Vector3.zero);

        public WallData CreateWall() =>
            new();

        public void PaintBlocks()
        {
            FieldData fieldData = _currentDataService.FieldData;
            PaintedBlockConfig config = _staticDataService.Get<PaintedBlockConfig>();

            for (int i = 0; i < config.Coordinates.Count; i++)
            {
                var coordinates = config.Coordinates[i];

                CellData cellData = fieldData.GetCellData(coordinates);
                cellData.BlockData.Paint();
            }
        }
    }
}