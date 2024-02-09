using Infrastructure.Services.StaticDataServices;

namespace Games
{
  public class GameConfig : IStaticData
  {
    public int ThroneHealth { get; private set; } = 100;
    public float SpawnCooldown { get; private set; } = 1f;
    public int WaveMobCount { get; private set; } = 10;
  }
}