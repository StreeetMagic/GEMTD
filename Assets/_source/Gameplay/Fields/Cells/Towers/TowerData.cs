using Gameplay.Fields.Cells.Towers.Shooters;

namespace Gameplay.Fields.Cells.Towers
{
    public class TowerData
    {
        public TowerType Type { get; set; }
        public int Level { get; set; }
        public IShooter Shooter { get; set; }

        public TowerData(TowerType type, int level, IShooter shooter)
        {
            Type = type;
            Level = level;
            Shooter = shooter;
        }
    }
}