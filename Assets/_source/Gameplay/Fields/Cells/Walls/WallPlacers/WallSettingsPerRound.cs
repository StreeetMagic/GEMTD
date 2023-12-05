using System;
using System.Collections.Generic;

namespace Gameplay.Fields.Cells.Walls.WallPlacers
{
    [Serializable]
    public class WallSettingsPerRound
    {
        public int RoundNumber;
        public List<Coordinates> DestroyList = new();
        public List<Coordinates> PlaceList = new();

        public int PlaceCount => PlaceList.Count;

        public WallSettingsPerRound(int roundNumber)
        {
            RoundNumber = roundNumber;
        }
    }
}