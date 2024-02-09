using Gameplay.Fields.Towers.Shooters;
using Gameplay.Fields.Towers.TargetDetectors;

namespace Gameplay.Fields.Towers
{
  public class TowerModel
  {
    public TowerModel(TowerType type, IShooter shooter, TargetDetetcorModel targetDetetcor, int level)
    {
      Type = type;
      TargetDetetcor = targetDetetcor;
      Level = level;
      Shooter = shooter;
    }

    public TowerType Type { get; set; }
    public TargetDetetcorModel TargetDetetcor { get; set; }
    public IShooter Shooter { get; set; }
    public int Level { get; set; }
  }
}