using System.Collections.Generic;
using Gameplay.Fields.Towers;

namespace Gameplay.Players
{
    public class PlayerModel
    {
        private Dictionary<int, List<int>> _chances = new Dictionary<int, List<int>>()
        {
            { 1, new List<int>() { 1, 2, 3, 4, 5 } },
            { 2, new List<int>() { 1, 2, 3, 4, 5 } },
            { 3, new List<int>() { 1, 2, 3, 4, 5 } },
            { 4, new List<int>() { 1, 2, 3, 4, 5 } },
            { 5, new List<int>() { 1, 2, 3, 4, 5 } }
        };
        
        public int Level { get; set; } = 1;

        public List<TowerType> TowerTypes(out List<int> levels)
        {
            levels = new List<int>()
            {
                1,
                2,
                3,
                4,
                5
            };

            return new List<TowerType>()
            {
                TowerType.B1,
                TowerType.B2,
                TowerType.B3,
                TowerType.B4,
                TowerType.B5
            };
        }
    }
}