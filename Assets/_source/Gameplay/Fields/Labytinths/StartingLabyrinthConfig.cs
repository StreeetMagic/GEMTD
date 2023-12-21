using System;
using System.Linq;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields.Labytinths
{
    public class StartingLabyrinthConfig : IStaticData
    {
        private Vector2Int[] _coordinates = Array.Empty<Vector2Int>();

        public Vector2Int[] Coordinates => _coordinates.ToArray();
    }
}