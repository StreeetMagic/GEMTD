using System.Linq;
using Gameplay.Fields.Checkpoints;

namespace Gameplay.Fields.Cells.CellsContainers
{
    public class CellsContainerModel
    {
        public CellsContainerModel(CellModel[] cellModels)
        {
            CellModels = cellModels;
        }

        public CellModel[] CellModels { get; set; }

        public CellModel GetCellModel(CoordinatesValues coordinatesValues) =>
            CellModels.FirstOrDefault(cellData => cellData.CoordinatesValues.Equals(coordinatesValues));

        public CellModel GetCellModelByCoordinates(CoordinatesValues coordinatesValues) =>
            CellModels.FirstOrDefault(cellData => cellData.CoordinatesValues.X == coordinatesValues.X && cellData.CoordinatesValues.Z == coordinatesValues.Z);

        public CheckPointModel[] GetCheckPointModels() =>
            CellModels
                .Where(cellModel => cellModel.HasCheckPoint)
                .Select(cellModel => cellModel.CheckPointModel)
                .OrderBy(checkPoint => checkPoint.Number)
                .ToArray();
    }
}