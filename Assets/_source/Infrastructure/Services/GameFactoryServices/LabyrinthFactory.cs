using Gameplay.BlockGrids;
using Gameplay.BlockGrids.Cells;
using Gameplay.BlockGrids.Labytinths;
using InfastuctureCore.Services.AssetProviderServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices.Factories;
using Infrastructure.Services.StaticDataServices;

namespace Infrastructure.Services.GameFactoryServices
{
    public class LabyrinthFactory
    {
        private readonly IAssetProviderService _assetProviderService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICurrentDataService _currentDataService;
        private readonly BlockGridFactory _blockGridFactory;

        public LabyrinthFactory(IAssetProviderService assetProviderService, IStaticDataService staticDataService, ICurrentDataService currentDataService, BlockGridFactory blockGridFactory)
        {
            _assetProviderService = assetProviderService;
            _staticDataService = staticDataService;
            _currentDataService = currentDataService;
            _blockGridFactory = blockGridFactory;
        }

        public void CreateStartingLabyrinth()
        {
            var blockGridData = _currentDataService.Get<BlockGridData>();
            Coordinates[] coordinates = _staticDataService.Get<StartingLabyrinthConfig>().Coordinates;

            foreach (Coordinates coordinate in coordinates)
            {
                var cellData = blockGridData.GetCellDataByCoordinates(coordinate);
                _blockGridFactory.CreateWall(cellData);
            }
        }
    }
}