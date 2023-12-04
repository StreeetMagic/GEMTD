namespace Gameplay.Fields.Towers.Resources
{
    public class TowerData
    {
        public TowerType Type { get; set; }
        public int Level { get; set; }

        public TowerData(TowerType type, int level)
        {
            Type = type;
            Level = level;
        }
    }
}