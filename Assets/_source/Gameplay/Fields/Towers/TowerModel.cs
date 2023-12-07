using Gameplay.Fields.Towers.Shooters;
using Gameplay.Fields.Towers.TargetDetectors;

namespace Gameplay.Fields.Towers
{
    public class TowerModel
    {
        public TowerType Type { get; set; }
        public int Level { get; set; }

        public TargetDetetcorModel TargetDetetcor { get; set; }
        public IShooter Shooter { get; set; }

        public TowerModel(TowerType type, int level, IShooter shooter, TargetDetetcorModel targetDetetcor)
        {
            Type = type;
            Level = level;
            TargetDetetcor = targetDetetcor;
            Shooter = shooter;
        }
    }
}