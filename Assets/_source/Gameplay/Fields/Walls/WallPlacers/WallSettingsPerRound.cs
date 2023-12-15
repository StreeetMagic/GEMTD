using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fields.Walls.WallPlacers
{
    [Serializable]
    public class WallSettingsPerRound
    {
        public int RoundNumber;
        public List<Vector2Int> DestroyList = new();
        public List<Vector2Int> PlaceList = new();

        public int PlaceCount => PlaceList.Count;

        public WallSettingsPerRound(int roundNumber)
        {
            RoundNumber = roundNumber;
        }
    }
}