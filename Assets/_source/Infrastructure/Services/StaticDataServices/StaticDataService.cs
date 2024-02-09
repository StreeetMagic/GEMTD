using Gameplay.Fields;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Walls.WallPlacers;
using Games;
using Infrastructure.DIC;

namespace Infrastructure.Services.StaticDataServices
{
  public class StaticDataService : IStaticDataService
  {
    private readonly IGodFactory _godFactory;

    public StaticDataService(IGodFactory godFactory)
    {
      _godFactory = godFactory;
    }

    public void RegisterConfigs()
    {
      EnemiesConfig = _godFactory.Create<EnemiesConfig>();
      CheckpointsConfig = _godFactory.Create<CheckpointsConfig>();
      TowersConfig = _godFactory.Create<TowersConfig>();
      GameConfig = _godFactory.Create<GameConfig>();
      FieldConfig = _godFactory.Create<FieldConfig>();
      StartingLabyrinthConfig = _godFactory.Create<StartingLabyrinthConfig>();
      WallPlacerConfig = _godFactory.Create<WallPlacerConfig>();

      TowersConfig.SetValues();
    }

    public EnemiesConfig EnemiesConfig { get; private set; }
    public CheckpointsConfig CheckpointsConfig { get; private set; }
    public GameConfig GameConfig { get; private set; }
    public TowersConfig TowersConfig { get; private set; }
    public FieldConfig FieldConfig { get; private set; }
    public StartingLabyrinthConfig StartingLabyrinthConfig { get; private set; }
    public WallPlacerConfig WallPlacerConfig { get; private set; }
  }
}