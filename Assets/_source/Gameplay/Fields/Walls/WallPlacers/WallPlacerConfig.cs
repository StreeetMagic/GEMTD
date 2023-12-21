using System.Collections.Generic;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;
using UnityEngine;

namespace Gameplay.Fields.Walls.WallPlacers
{
    public class WallSettingsPerRound
    {
        public int RoundNumber;
        public readonly List<Vector2Int> DestroyList = new();
        public List<Vector2Int> PlaceList = new();

        public int PlaceCount => PlaceList.Count;
    }

    public class WallPlacerConfig : IStaticData
    {
        public int TowerPerRound { get; } = 5;

        public List<WallSettingsPerRound> WallSettingsPerRounds = new List<WallSettingsPerRound>()
        {
            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 0)
                .With(e => e.PlaceList.Add(new Vector2Int(0, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(1, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(2, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(3, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(4, 9))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 1)
                .With(e => e.PlaceList.Add(new Vector2Int(5, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(6, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(7, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(9, 9))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 2)
                .With(e => e.PlaceList.Add(new Vector2Int(7, 8)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 8)))
                .With(e => e.PlaceList.Add(new Vector2Int(9, 8)))
                .With(e => e.PlaceList.Add(new Vector2Int(7, 7)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 7))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 3)
                .With(e => e.PlaceList.Add(new Vector2Int(9, 7)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 0)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 1)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 2)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 3))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 4)
                .With(e => e.PlaceList.Add(new Vector2Int(13, 4)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 5)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 6)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 7)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 8))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 5)
                .With(e => e.PlaceList.Add(new Vector2Int(14, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(15, 10)))
                .With(e => e.PlaceList.Add(new Vector2Int(15, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(15, 12)))
                .With(e => e.PlaceList.Add(new Vector2Int(15, 13))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 6)
                .With(e => e.PlaceList.Add(new Vector2Int(15, 14)))
                .With(e => e.PlaceList.Add(new Vector2Int(14, 15)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 14)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(13, 12))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 7)
                .With(e => e.PlaceList.Add(new Vector2Int(13, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(12, 10)))
                .With(e => e.PlaceList.Add(new Vector2Int(11, 9)))
                .With(e => e.PlaceList.Add(new Vector2Int(11, 8)))
                .With(e => e.PlaceList.Add(new Vector2Int(11, 7))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 8)
                .With(e => e.PlaceList.Add(new Vector2Int(11, 6)))
                .With(e => e.PlaceList.Add(new Vector2Int(10, 5)))
                .With(e => e.PlaceList.Add(new Vector2Int(9, 5)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 5)))
                .With(e => e.PlaceList.Add(new Vector2Int(7, 5))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 9)
                .With(e => e.PlaceList.Add(new Vector2Int(6, 5)))
                .With(e => e.PlaceList.Add(new Vector2Int(5, 6)))
                .With(e => e.PlaceList.Add(new Vector2Int(5, 7)))
                .With(e => e.PlaceList.Add(new Vector2Int(3, 8)))
                .With(e => e.PlaceList.Add(new Vector2Int(3, 7))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 10)
                .With(e => e.PlaceList.Add(new Vector2Int(3, 6)))
                .With(e => e.PlaceList.Add(new Vector2Int(3, 5)))
                .With(e => e.PlaceList.Add(new Vector2Int(4, 4)))
                .With(e => e.PlaceList.Add(new Vector2Int(5, 3)))
                .With(e => e.PlaceList.Add(new Vector2Int(6, 3))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 11)
                .With(e => e.PlaceList.Add(new Vector2Int(7, 3)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 3)))
                .With(e => e.PlaceList.Add(new Vector2Int(9, 3)))
                .With(e => e.PlaceList.Add(new Vector2Int(10, 3)))
                .With(e => e.PlaceList.Add(new Vector2Int(11, 3))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 12)
                .With(e => e.PlaceList.Add(new Vector2Int(11, 10)))
                .With(e => e.PlaceList.Add(new Vector2Int(10, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(9, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(6, 10))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 13)
                .With(e => e.PlaceList.Add(new Vector2Int(6, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(6, 12)))
                .With(e => e.PlaceList.Add(new Vector2Int(7, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(9, 13))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 14)
                .With(e => e.PlaceList.Add(new Vector2Int(10, 14)))
                .With(e => e.PlaceList.Add(new Vector2Int(10, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(11, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(11, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(12, 11))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 15)
                .With(e => e.PlaceList.Add(new Vector2Int(9, 15)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 15)))
                .With(e => e.PlaceList.Add(new Vector2Int(7, 15)))
                .With(e => e.PlaceList.Add(new Vector2Int(6, 15)))
                .With(e => e.PlaceList.Add(new Vector2Int(5, 14))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 16)
                .With(e => e.PlaceList.Add(new Vector2Int(4, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(4, 12)))
                .With(e => e.PlaceList.Add(new Vector2Int(4, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(3, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(2, 11))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 17)
                .With(e => e.PlaceList.Add(new Vector2Int(1, 11)))
                .With(e => e.PlaceList.Add(new Vector2Int(0, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(1, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(2, 13)))
                .With(e => e.PlaceList.Add(new Vector2Int(11, 14))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 18)
                .With(e => e.PlaceList.Add(new Vector2Int(11, 2)))
                .With(e => e.PlaceList.Add(new Vector2Int(11, 1)))
                .With(e => e.PlaceList.Add(new Vector2Int(10, 1)))
                .With(e => e.PlaceList.Add(new Vector2Int(9, 1)))
                .With(e => e.PlaceList.Add(new Vector2Int(8, 1))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 19)
                .With(e => e.PlaceList.Add(new Vector2Int(7, 1)))
                .With(e => e.PlaceList.Add(new Vector2Int(6, 1)))
                .With(e => e.PlaceList.Add(new Vector2Int(5, 1)))
                .With(e => e.PlaceList.Add(new Vector2Int(4, 1)))
                .With(e => e.PlaceList.Add(new Vector2Int(3, 2))),

            new WallSettingsPerRound()
                .With(e => e.RoundNumber = 20)
                .With(e => e.PlaceList.Add(new Vector2Int(2, 3)))
                .With(e => e.PlaceList.Add(new Vector2Int(1, 4)))
                .With(e => e.PlaceList.Add(new Vector2Int(1, 5)))
                .With(e => e.PlaceList.Add(new Vector2Int(1, 6)))
                .With(e => e.PlaceList.Add(new Vector2Int(1, 7))),
        };
    }
}