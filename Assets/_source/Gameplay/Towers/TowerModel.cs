using Gameplay.Towers.Shooters;

namespace Gameplay.Towers
{
    public class TowerModel
    {
        public TowerType Type { get; set; }
        public int Level { get; set; }
        public IShooter Shooter { get; set; }

        public TowerModel(TowerType type, int level, IShooter shooter)
        {
            Type = type;
            Level = level;
            Shooter = shooter;
        }
    }
}