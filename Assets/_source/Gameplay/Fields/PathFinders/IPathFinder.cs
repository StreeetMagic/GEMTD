using System.Collections.Generic;
using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.PathFinders
{
  public interface IPathFinder
  {
    void FindPath(CellModel[] field, Vector2Int startCoordinatesValues, Vector2Int finishCoordinates, List<Vector2Int> path);
  }
}