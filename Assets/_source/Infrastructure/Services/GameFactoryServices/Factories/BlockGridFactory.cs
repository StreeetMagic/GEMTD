using Gameplay.BlockGrids;
using Gameplay.BlockGrids.Blocks;
using Gameplay.BlockGrids.Cells;
using Gameplay.BlockGrids.Checkpoints;
using Gameplay.BlockGrids.Walls;
using Games;
using InfastuctureCore.Services.AssetProviderServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.StaticDataServices;
using UnityEngine;
using BlockGridData = Gameplay.BlockGrids.BlockGridData;

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

        public BlockGridData CreateBlockGridData()
        {
            int xSize = _staticDataService.Get<BlockGridConfig>().FieldXSize;
            int ySize = _staticDataService.Get<BlockGridConfig>().FieldYSize;

            CellData[] cellDatas = CreateCellDatas(xSize, ySize);

            var blockGridData = new BlockGridData(cellDatas);
            _currentDataService.Register(blockGridData);
            CreateBlockGridView(blockGridData);
            CreateCheckpointsDatas();
            return blockGridData;
        }

        public BlockView CreateBlockView(BlockData blockData, Transform parent)
        {
            var blockView = _assetProviderService.Instantiate<BlockView>(Constants.AssetsPath.Prefabs.Block, Vector3.zero);
            blockView.Init(blockData);
            blockView.transform.SetParent(parent);
            blockView.transform.localPosition = Vector3.zero;
            return blockView;
        }

        public CheckpointView CreateCheckpointView(CheckpointData checkpointData, Transform transform)
        {
            var checkpointView = _assetProviderService.Instantiate<CheckpointView>(Constants.AssetsPath.Prefabs.Checkpoint, Vector3.zero);
            checkpointView.Init(checkpointData);
            checkpointView.transform.SetParent(transform);
            checkpointView.transform.localPosition = Vector3.zero;
            checkpointView.name = "Checkpoint " + checkpointData.Number;
            return checkpointView;
        }

        public CellView CreateCellView(CellData cellData, Transform transform)
        {
            var cellView = _assetProviderService.Instantiate<CellView>(Constants.AssetsPath.Prefabs.Cell, Vector3.zero);
            cellView.Init(cellData);
            cellView.transform.SetParent(transform);
            cellView.transform.localPosition = new Vector3(cellData.Coordinates.X, 0, cellData.Coordinates.Z);
            cellView.name = "Cell (" + cellData.Coordinates.X + ", " + cellData.Coordinates.Z + ")";
            return cellView;
        }

        private void CreateCheckpointsDatas()
        {
            CheckpointSettings[] configs = _staticDataService.Get<CheckpointsConfig>().CheckPointSettings;

            CheckpointData[] checkpointDatas = new CheckpointData[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                checkpointDatas[i] = new CheckpointData(configs[i].Number);
                CellData cell = GetCellDataByCoordinates(configs[i].Coordinates);
                cell.SetCheckpointData(checkpointDatas[i]);
            }
        }

        private void CreateBlockGridView(BlockGridData blockGridData)
        {
            var blockGridView = _assetProviderService.Instantiate<BlockGridView>(Constants.AssetsPath.Prefabs.BlockGrid);
            blockGridView.Init(blockGridData);
        }

        private CellData[] CreateCellDatas(int xSize, int ySize)
        {
            CellData[] cellDatas = new CellData[xSize * ySize];

            int count = 0;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                    cellDatas[count++] = new CellData(new Coordinates(i, j), new BlockData());
            }

            return cellDatas;
        }

        private CellData GetCellDataByCoordinates(Coordinates coordinates)
        {
            CellData[] allCellDatas = _currentDataService.Get<BlockGridData>().CellDatas;

            foreach (CellData cellData in allCellDatas)
            {
                if (cellData.Coordinates.X == coordinates.X && cellData.Coordinates.Z == coordinates.Z)
                {
                    return cellData;
                }
            }

            return null;
        }

        public WallView CreateWallView(WallData wallData, Transform transform)
        {
            var wallView = _assetProviderService.Instantiate<WallView>(Constants.AssetsPath.Prefabs.Wall, Vector3.zero);
            wallView.Init(wallData);
            wallView.transform.SetParent(transform);
            wallView.transform.localPosition = Vector3.zero;
            return wallView;
        }

        public WallData CreateWall(CellData cellData)
        {
            WallData wallData = new WallData();
            cellData.SetWallData(wallData);
            return wallData;
        }
    }
}