using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.BlockGrids.Cells;
using Sirenix.OdinInspector;
using UnityEngine;

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