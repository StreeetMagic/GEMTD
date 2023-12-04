using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.Walls;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameDesign
{
    public enum GameDesignMode
    {
        StartingWallsPlacing = 0,
        PaintingBlocks = 1,
        HighlightingCells = 2,
        WallPacersSetup = 3
    }
    
    public class LevelDesignTools : MonoBehaviour
    {
        public MapWallsConfig MapWallsConfig;
        public PaintedBlockConfig PaintedBlockConfig;
        public StartingLabyrinthConfig StartingLabyrinthConfig;
        public GameDesignMode GameDesignMode;

        [Button]
        public void SaveWallCoordinates()
        {
            MapWallsConfig.Coordinates = new List<Coordinates>();
            WallView[] wallViews = FindObjectsOfType<WallView>();

            Coordinates[] coordinates = new Coordinates[wallViews.Length];

            for (int i = 0; i < wallViews.Length; i++)
            {
                WallView wallView = wallViews[i];
                coordinates[i] = wallView.GetComponentInParent<CellView>().CelLData.Coordinates;
            }

            MapWallsConfig.Coordinates = coordinates.ToList();
        }

        [Button]
        public void SetWallCoordinates()
        {
            StartingLabyrinthConfig.SetCoordinates(MapWallsConfig.Coordinates.ToArray());
        }

        [Button]
        public void SavePaintedBlockCoordinates()
        {
            PaintedBlockConfig.Coordinates = new List<Coordinates>();
            List<CellView> cellViews = FindObjectsOfType<CellView>().ToList();

            foreach (var cellView in cellViews)
            {
                if (cellView.IsPainted)
                {
                    PaintedBlockConfig.Coordinates.Add(cellView.CelLData.Coordinates);
                }
            }
        }
    }
}