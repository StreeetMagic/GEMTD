using System;
using System.Collections.Generic;
using Gameplay.Fields.Cells;

namespace Gameplay.Fields.Walls.WallPlacers
{
    [Serializable]
    public class WallSettingsPerRound
    {
        public int RoundNumber;
        public List<CoordinatesValues> DestroyList = new();
        public List<CoordinatesValues> PlaceList = new();

        public int PlaceCount => PlaceList.Count;

        public WallSettingsPerRound(int roundNumber)
        {
            RoundNumber = roundNumber;
        }
    }
}