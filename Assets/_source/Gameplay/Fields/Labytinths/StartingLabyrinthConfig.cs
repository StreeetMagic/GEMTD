using System.Linq;
using GameDesign;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services;
using UnityEngine;
using IStaticDataService = InfastuctureCore.Services.StaticDataServices.IStaticDataService;

namespace Gameplay.Fields.Labytinths
{
    [CreateAssetMenu(menuName = "Configs/Starting Labyrinth Config", fileName = "StartingLabyrinthConfig")]
    public class StartingLabyrinthConfig : ScriptableObject, IStaticData
    {
        public Coordinates[] _coordinates;

        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();
        private MapWallsConfig MapWallsConfig => StaticDataService.Get<MapWallsConfig>();

        public void SetCoordinates(Coordinates[] coordinates)
        {
            _coordinates = coordinates;
        }

        public Coordinates[] Coordinates => _coordinates.ToArray();
    }
}