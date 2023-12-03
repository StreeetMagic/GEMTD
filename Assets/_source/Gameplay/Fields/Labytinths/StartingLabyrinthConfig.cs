using System.Linq;
using GameDesign;
using Gameplay.Fields.Cells;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using IStaticDataService = InfastuctureCore.Services.StaticDataServices.IStaticDataService;

namespace Gameplay.Fields.Labytinths
{
    [CreateAssetMenu(menuName = "Configs/Starting Labyrinth Config", fileName = "StartingLabyrinthConfigSO")]
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