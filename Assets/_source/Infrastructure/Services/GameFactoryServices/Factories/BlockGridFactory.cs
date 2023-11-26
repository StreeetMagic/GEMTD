using Games;
using Games.Config.Resources;
using InfastuctureCore.Services.AssetProviderServices;
using Infrastructure.Services.CurrentDataServices;
using UnityEngine;

namespace Infrastructure.Services.GameFactoryServices
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

        public void CreateBlockGrid()
        {
            BlockGridData blockGridData = _currentDataService.Register(CreateBlockGridData());
            var blockGridView = _assetProviderService.Instantiate<BlockGridView>(Constants.AssetsPath.Prefabs.BlockGrid);
            CellView[] cellViews = CreateBlockCells(blockGridData.CellDatas, blockGridView.CellsContainer.transform);
            blockGridView.Init(blockGridData, cellViews);
        }

        private CellView[] CreateBlockCells(CellData[] cellDatas, Transform parent)
        {
            CellView[] cellViews = new CellView[cellDatas.Length];

            for (int i = 0; i < cellDatas.Length; i++)
            {
                CellData cellData = cellDatas[i];
                var position = new Vector3(cellData.Coordinates.X, 0, cellData.Coordinates.Z);
                var cellView = _assetProviderService.Instantiate<CellView>(Constants.AssetsPath.Prefabs.Cell, position);
                cellView.transform.SetParent(parent);
                cellViews[i] = cellView;
                var block = CreateBlock(position, cellView.transform);
                cellView.Init(block);
            }

            return cellViews;
        }

        private BlockView CreateBlock(Vector3 at, Transform parent)
        {
            var blockView = _assetProviderService.Instantiate<BlockView>(Constants.AssetsPath.Prefabs.Block, at);
            blockView.transform.SetParent(parent);
            return blockView;
        }

        private BlockGridData CreateBlockGridData()
        {
            int xSize = _staticDataService.Get<GameConfig>().FieldXSize;
            int ySize = _staticDataService.Get<GameConfig>().FieldYSize;

            CellData[] cellDatas = new CellData[xSize * ySize];

            int count = 0;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                    cellDatas[count++] = new CellData(new Coordinates(i, j));
            }

            return new BlockGridData(cellDatas);
        }
    }
}