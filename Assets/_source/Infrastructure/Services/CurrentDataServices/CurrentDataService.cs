using Gameplay.Fields;
using InfastuctureCore.Services;

namespace Infrastructure.Services.CurrentDataServices
{
    public interface ICurrentDataService : IService
    {
        public FieldData FieldData { get; set; }
    }

    public class CurrentDataService : ICurrentDataService
    {
        public FieldData FieldData { get; set; }
    }
}