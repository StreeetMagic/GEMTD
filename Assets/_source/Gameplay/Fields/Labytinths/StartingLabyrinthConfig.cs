using System.Linq;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;

namespace Gameplay.Fields.Labytinths
{
    [CreateAssetMenu(menuName = "Configs/Starting Labyrinth Config", fileName = "StartingLabyrinthConfig")]
    public class StartingLabyrinthConfig : ScriptableObject, IStaticData
    {
        public CoordinatesValues[] _coordinates;

        public void SetCoordinates(CoordinatesValues[] coordinates)
        {
            _coordinates = coordinates;
        }

        public CoordinatesValues[] Coordinates => _coordinates.ToArray();
    }
}