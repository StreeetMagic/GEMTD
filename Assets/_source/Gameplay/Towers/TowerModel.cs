using Gameplay.Towers.Shooters;
using Gameplay.Towers.TargetDetectors;

namespace Gameplay.Towers
{
    public class TowerModel
    {
        public TowerType Type { get; set; }
        public int Level { get; set; }
        public IShooter Shooter { get; set; }
        public TargetDetetcorModel TargetDetetcor { get; set; }

        public TowerModel(TowerType type, int level, IShooter shooter, TargetDetetcorModel targetDetetcor)
        {
            Type = type;
            Level = level;
            Shooter = shooter;
            TargetDetetcor = targetDetetcor;
        }
    }
}