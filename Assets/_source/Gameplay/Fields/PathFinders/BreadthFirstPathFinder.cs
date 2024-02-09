using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.PathFinders
{
  public class BreadthFirstPathFinder : IPathFinder
  {
    #region IPathFinder Members

    public void FindPath(CellModel[] field, Vector2Int startCoordinates, Vector2Int finishCoordinates, List<Vector2Int> path)
    {
      List<Cell> allCells = InitializeCells(field);

      Cell startCell = allCells.FirstOrDefault(cell => cell.Coordinates.Equals(startCoordinates));
      Cell finishCell = allCells.FirstOrDefault(cell => cell.Coordinates.Equals(finishCoordinates));

      if (startCell == null || finishCell == null)
        return;

      SetInitialValues(startCell);

      var queue = new Queue<Cell>();
      queue.Enqueue(startCell);

      while (queue.Count > 0)
      {
        Cell currentCell = queue.Dequeue();

        if (currentCell.Coordinates.Equals(finishCoordinates))
        {
          RetrievePath(allCells, finishCell, path);
          return;
        }

        UpdateNeighbours(currentCell, allCells, queue);
      }
    }

    #endregion

    private List<Cell> InitializeCells(CellModel[] field) =>
      field.Select(cellModel => new Cell(cellModel.Coordinates, cellModel.IsPassable)).ToList();

    private void SetInitialValues(Cell startCell)
    {
      startCell.Distance = 0;
      startCell.IsVisited = true;
    }

    private void UpdateNeighbours(Cell currentCell, List<Cell> allCells, Queue<Cell> queue)
    {
      Cell[] neighbours = GetCellNeighbours(currentCell, allCells.ToArray());

      foreach (Cell neighbour in neighbours)
      {
        if (neighbour.IsVisited || !neighbour.IsPassable)
          continue;

        neighbour.IsVisited = true;
        neighbour.Distance = currentCell.Distance + 1;
        queue.Enqueue(neighbour);
      }
    }

    private void RetrievePath(List<Cell> allCells, Cell finishCell, List<Vector2Int> path)
    {
      foreach (Cell visitedCell in allCells)
      {
        visitedCell.IsVisited = false;
      }

      finishCell.IsVisited = true;

      int distance = finishCell.Distance;
      var reversePath = new List<Cell> { finishCell };

      while (true)
      {
        Cell[] localNeighbours = GetCellNeighbours(finishCell, allCells.ToArray());

        foreach (Cell localNeighbour in localNeighbours)
        {
          if (localNeighbour.Distance != distance - 1)
            continue;

          if (localNeighbour.Distance == 0)
          {
            reversePath.Add(localNeighbour);
            reversePath.Reverse();
            path.AddRange(reversePath.Select(cell => cell.Coordinates));
            return;
          }

          distance--;
          finishCell = localNeighbour;
          reversePath.Add(localNeighbour);
          break;
        }
      }
    }

    private bool TryGetCell(List<Cell> allCells, Vector2Int coordinatesValues, out Cell thisCell)
    {
      thisCell = allCells.FirstOrDefault(cell => cell.Coordinates.x == coordinatesValues.x && cell.Coordinates.y == coordinatesValues.y);
      return thisCell != null;
    }

    private Cell[] GetCellNeighbours(Cell cell, Cell[] field)
    {
      Vector2Int cellCoordinates = cell.Coordinates;
      var fieldList = new List<Cell>(field);

      var directions = new List<Vector2Int>
      {
        new(-1, 0),
        new(1, 0),
        new(0, -1),
        new(0, 1)
      };

      var neighbours = new List<Cell>();

      foreach (Vector2Int neighbourCoords in directions.Select(direction => new Vector2Int(cellCoordinates.x + direction.x, cellCoordinates.y + direction.y)))
      {
        if (TryGetCell(fieldList, neighbourCoords, out Cell neighbourCell) && !neighbourCell.IsVisited && neighbourCell.IsPassable)
          neighbours.Add(neighbourCell);
      }

      return neighbours.ToArray();
    }

    #region Nested type: Cell

    private class Cell
    {
      public Vector2Int Coordinates;
      public int Distance;
      public readonly bool IsPassable;
      public bool IsVisited;

      public Cell(Vector2Int coordinates, bool isPassable)
      {
        Coordinates = coordinates;
        IsPassable = isPassable;
      }
    }

    #endregion
  }
}