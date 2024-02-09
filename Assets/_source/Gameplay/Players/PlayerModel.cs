using System.Collections.Generic;
using Gameplay.Fields.Towers;
using UnityEngine;

namespace Gameplay.Players
{
  public class PlayerModel
  {
    private readonly Dictionary<int, List<int>> _chances;
    private readonly Dictionary<TowerType, int> _towerLevels;
    private readonly Dictionary<int, List<TowerType>> _towers;

    public PlayerModel()
    {
      _chances = new Dictionary<int, List<int>>
      {
        {
          1, new List<int>
            { 100, 0, 0, 0, 0 }
        },
        {
          2, new List<int>
            { 80, 20, 0, 0, 0 }
        },
        {
          3, new List<int>
            { 60, 30, 10, 0, 0 }
        },
        {
          4, new List<int>
            { 40, 30, 20, 10, 0 }
        },
        {
          5, new List<int>
            { 10, 30, 20, 20, 10 }
        }
      };

      _towers = new Dictionary<int, List<TowerType>>
      {
        {
          1, new List<TowerType>
            { TowerType.B1, TowerType.D1, TowerType.Y1, TowerType.G1, TowerType.E1, TowerType.Q1, TowerType.R1, TowerType.P1 }
        },
        {
          2, new List<TowerType>
            { TowerType.B2, TowerType.D2, TowerType.Y2, TowerType.G2, TowerType.E2, TowerType.Q2, TowerType.R2, TowerType.P2 }
        },
        {
          3, new List<TowerType>
            { TowerType.B3, TowerType.D3, TowerType.Y3, TowerType.G3, TowerType.E3, TowerType.Q3, TowerType.R3, TowerType.P3 }
        },
        {
          4, new List<TowerType>
            { TowerType.B4, TowerType.D4, TowerType.Y4, TowerType.G4, TowerType.E4, TowerType.Q4, TowerType.R4, TowerType.P4 }
        },
        {
          5, new List<TowerType>
            { TowerType.B5, TowerType.D5, TowerType.Y5, TowerType.G5, TowerType.E5, TowerType.Q5, TowerType.R5, TowerType.P5 }
        }
      };

      _towerLevels = new Dictionary<TowerType, int>
      {
        { TowerType.B1, 1 },
        { TowerType.B2, 2 },
        { TowerType.B3, 3 },
        { TowerType.B4, 4 },
        { TowerType.B5, 5 },

        { TowerType.D1, 1 },
        { TowerType.D2, 2 },
        { TowerType.D3, 3 },
        { TowerType.D4, 4 },
        { TowerType.D5, 5 },

        { TowerType.Y1, 1 },
        { TowerType.Y2, 2 },
        { TowerType.Y3, 3 },
        { TowerType.Y4, 4 },
        { TowerType.Y5, 5 },

        { TowerType.G1, 1 },
        { TowerType.G2, 2 },
        { TowerType.G3, 3 },
        { TowerType.G4, 4 },
        { TowerType.G5, 5 },

        { TowerType.E1, 1 },
        { TowerType.E2, 2 },
        { TowerType.E3, 3 },
        { TowerType.E4, 4 },
        { TowerType.E5, 5 },

        { TowerType.Q1, 1 },
        { TowerType.Q2, 2 },
        { TowerType.Q3, 3 },
        { TowerType.Q4, 4 },
        { TowerType.Q5, 5 },

        { TowerType.R1, 1 },
        { TowerType.R2, 2 },
        { TowerType.R3, 3 },
        { TowerType.R4, 4 },
        { TowerType.R5, 5 },

        { TowerType.P1, 1 },
        { TowerType.P2, 2 },
        { TowerType.P3, 3 },
        { TowerType.P4, 4 },
        { TowerType.P5, 5 }
      };
    }

    public int Level { get; set; } = 5;

    public List<TowerType> TowerTypes(out List<int> levels)
    {
      levels = new List<int>();
      var towers = new List<TowerType>();
      List<int> chances = _chances[Level];

      for (var j = 0; j < chances.Count; j++)
      {
        int chance = chances[j];

        for (var i = 0; i < chance; i++)
        {
          TowerType towerType = _towers[j + 1][Random.Range(0, _towers[j + 1].Count)];
          towers.Add(towerType);
          levels.Add(_towerLevels[towerType]);
        }
      }

      return towers;
    }
  }
}