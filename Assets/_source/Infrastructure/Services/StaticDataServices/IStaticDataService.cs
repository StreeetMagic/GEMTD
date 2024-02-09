using Gameplay.Fields;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Walls.WallPlacers;
using Games;

namespace Infrastructure.Services.StaticDataServices
{
  public interface IStaticDataService : IService
  {
    EnemiesConfig EnemiesConfig { get; }
    CheckpointsConfig CheckpointsConfig { get; }
    GameConfig GameConfig { get; }
    TowersConfig TowersConfig { get; }
    FieldConfig FieldConfig { get; }
    StartingLabyrinthConfig StartingLabyrinthConfig { get; }
    WallPlacerConfig WallPlacerConfig { get; }
    void RegisterConfigs();
  }
}