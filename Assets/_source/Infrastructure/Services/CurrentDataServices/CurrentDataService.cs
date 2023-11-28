using Gameplay.BlockGrids;
using InfastuctureCore.Services;

namespace Infrastructure.Services.CurrentDataServices
{
    public interface ICurrentDataService : IService
    {
        public BlockGridData BlockGridData { get; set; }
    }

    public class CurrentDataService : ICurrentDataService
    {
        public BlockGridData BlockGridData { get; set; }
    }
}