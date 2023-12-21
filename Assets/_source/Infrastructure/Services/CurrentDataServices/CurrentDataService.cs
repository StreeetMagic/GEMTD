using Gameplay.Fields;
using Gameplay.Fields.Thrones;
using Gameplay.Players;
using InfastuctureCore.Services;

namespace Infrastructure.Services.CurrentDataServices
{
    public interface ICurrentDataService : IService
    {
        public FieldModel FieldModel { get; set; }
        public ThroneModel ThroneModel { get; set; }
        public PlayerModel PlayerModel { get; set; }
    }

    public class CurrentDataService : ICurrentDataService
    {
        public FieldModel FieldModel { get; set; }
        public ThroneModel ThroneModel { get; set; }
        public PlayerModel PlayerModel { get; set; }
    }
}