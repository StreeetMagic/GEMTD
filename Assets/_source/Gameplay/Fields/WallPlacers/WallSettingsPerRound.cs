using System;
using System.Collections.Generic;
using Gameplay.Fields.Cells;

namespace Gameplay.Fields.WallPlacers
{
    [Serializable]
    public class WallSettingsPerRound
    {
        public int RoundNumber;
        public List<Coordinates> DestroyList = new List<Coordinates>();
        public List<Coordinates> PlaceList = new List<Coordinates>();

        public int PlaceCount => PlaceList.Count;

        public WallSettingsPerRound(int roundNumber)
        {
            RoundNumber = roundNumber;
        }
    }
}