using System.Linq;
using Gameplay.Fields.Checkpoints;
using UnityEngine;

namespace Gameplay.Fields.Cells.CellsContainers
{
    public class CellsContainerModel
    {
        public CellsContainerModel(CellModel[] cellModels)
        {
            CellModels = cellModels;
        }

        public CellModel[] CellModels { get; set; }

        public CellModel GetCellModel(Vector2Int coordinatesValues) =>
            CellModels.FirstOrDefault(cellData => cellData.CoordinatesValues.Equals(coordinatesValues));

        public CellModel GetCellModelByCoordinates(Vector2Int coordinatesValues) =>
            CellModels.FirstOrDefault(cellData => cellData.CoordinatesValues.x == coordinatesValues.x && cellData.CoordinatesValues.y == coordinatesValues.y);

        public CheckPointModel[] GetCheckPointModels() =>
            CellModels
                .Where(cellModel => cellModel.HasCheckPoint)
                .Select(cellModel => cellModel.CheckPointModel)
                .OrderBy(checkPoint => checkPoint.Number)
                .ToArray();
    }
}