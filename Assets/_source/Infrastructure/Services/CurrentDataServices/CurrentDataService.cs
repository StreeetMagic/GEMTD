using Gameplay.Fields;
using InfastuctureCore.Services;

namespace Infrastructure.Services.CurrentDataServices
{
    public interface ICurrentDataService : IService
    {
        public FieldModel FieldModel { get; set; }
    }

    public class CurrentDataService : ICurrentDataService
    {
        public FieldModel FieldModel { get; set; }
    }
}