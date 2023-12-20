using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Checkpoints;
using UnityEngine;

namespace Gameplay.Fields.CellsContainers
{
    public class CellsContainerModel
    {
        public CellsContainerModel(CellModel[] cellModels)
        {
            CellModels = cellModels;
        }

        public CellModel[] CellModels { get; set; }

        public CellModel GetCellModel(Vector2Int coordinatesValues) =>
            CellModels.FirstOrDefault(cellData => cellData.Coordinates.Equals(coordinatesValues));

        public CellModel GetCellModelByCoordinates(Vector2Int coordinatesValues) =>
            CellModels.FirstOrDefault(cellData => cellData.Coordinates.x == coordinatesValues.x && cellData.Coordinates.y == coordinatesValues.y);

        public CheckPointModel[] GetCheckPointModels() =>
            CellModels
                .Where(cellModel => cellModel.HasCheckPoint)
                .Select(cellModel => cellModel.CheckPointModel)
                .OrderBy(checkPoint => checkPoint.Number)
                .ToArray();
    }
}