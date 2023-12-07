using System.Collections.Generic;
using Gameplay.Fields.Cells;

namespace Gameplay.Fields.PathFinders
{
    public interface IPathFinder
    {
        CoordinatesValues[] FindPath(CellModel[] field, CoordinatesValues start, CoordinatesValues end);
    }

    public class BreadthFirstPathFinder : IPathFinder
    {
        public CoordinatesValues[] FindPath(CellModel[] field, CoordinatesValues start, CoordinatesValues end)
        {
            Dictionary<CoordinatesValues, bool> passable = new Dictionary<CoordinatesValues, bool>();

            foreach (CellModel cell in field)
            {
                passable.Add(cell.CoordinatesValues, cell.IsPassable);
            }

            List<CoordinatesValues> path = new List<CoordinatesValues>();

            Queue<CoordinatesValues> queue = new Queue<CoordinatesValues>();

            queue.Enqueue(start);

            return null;
        }
    }
}