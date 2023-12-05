using System.Linq;
using Gameplay.Fields.Cells;
using InfastuctureCore.Services;
using UnityEngine;

namespace Gameplay.Fields.Labytinths
{
    [CreateAssetMenu(menuName = "Configs/Starting Labyrinth Config", fileName = "StartingLabyrinthConfig")]
    public class StartingLabyrinthConfig : ScriptableObject, IStaticData
    {
        public Coordinates[] _coordinates;

        public void SetCoordinates(Coordinates[] coordinates)
        {
            _coordinates = coordinates;
        }

        public Coordinates[] Coordinates => _coordinates.ToArray();
    }
}