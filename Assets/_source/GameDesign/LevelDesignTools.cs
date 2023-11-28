using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameDesign
{
    public class LevelDesignTools : MonoBehaviour
    {
        public MapWallsConfig MapWallsConfig;

        [Button]
        public void GetWallCoordinates()
        {
            WallView[] wallViews = FindObjectsOfType<WallView>();

            Coordinates[] coordinates = new Coordinates[wallViews.Length];

            for (int i = 0; i < wallViews.Length; i++)
            {
                WallView wallView = wallViews[i];
                coordinates[i] = wallView.GetComponentInParent<CellView>().CelLData.Coordinates;
            }

            MapWallsConfig.Coordinates = coordinates.ToList();
        }
    }
}