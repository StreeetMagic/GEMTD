using System.Linq;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;

namespace Gameplay.Fields.Labytinths
{
    [CreateAssetMenu(menuName = "Configs/Starting Labyrinth Config", fileName = "StartingLabyrinthConfig")]
    public class StartingLabyrinthConfig : ScriptableObject, IStaticData
    {
        public Vector2Int[] _coordinates;

        public void SetCoordinates(Vector2Int[] coordinates)
        {
            _coordinates = coordinates;
        }

        public Vector2Int[] Coordinates => _coordinates.ToArray();
    }
}