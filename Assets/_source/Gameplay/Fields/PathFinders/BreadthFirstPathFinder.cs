using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.PathFinders
{
    public class BreadthFirstPathFinder : IPathFinder
    {
        public void FindPath(CellModel[] field, Vector2Int startCoordinatesValues, Vector2Int finishCoordinates, List<Vector2Int> path)
        {
            List<Cell> allCells = new();
            Cell startCell = null;
            Cell finishCell = null;

            foreach (CellModel cellModel in field)
            {
                var item = new Cell(cellModel.Coordinates, cellModel.IsPassable);

                allCells.Add(item);

                if (cellModel.Coordinates.x == startCoordinatesValues.x && cellModel.Coordinates.y == startCoordinatesValues.y)
                    startCell = item;

                if (cellModel.Coordinates.x == finishCoordinates.x && cellModel.Coordinates.y == finishCoordinates.y)
                    finishCell = item;
            }

            if (startCell == null)
                return;

            startCell.Distance = 0;
            startCell.IsVisited = true;

            Queue<Cell> queue = new();

            queue.Enqueue(startCell);

            while (queue.Count > 0)
            {
                Cell cell = queue.Dequeue();
                cell.IsVisited = true;

                if (cell.Coordinates.x == finishCoordinates.x && cell.Coordinates.y == finishCoordinates.y)
                {
                    foreach (Cell visitedCell in allCells)
                    {
                        visitedCell.IsVisited = false;
                    }

                    cell.IsVisited = true;

                    int distance = cell.Distance;

                    List<Cell> locanReversePath = new()
                    {
                        finishCell
                    };

                    while (true)
                    {
                        Cell[] localNeighbours = GetCellNeighbours(cell, allCells.ToArray());

                        foreach (Cell localNeighbour in localNeighbours)
                        {
                            if (localNeighbour.Distance != distance - 1)
                                continue;

                            if (localNeighbour.Distance == 0)
                            {
                                locanReversePath.Add(localNeighbour);

                                locanReversePath.Reverse();

                                path.AddRange(locanReversePath.Select(localCell => localCell.Coordinates));

                                return;
                            }

                            distance--;
                            cell = localNeighbour;
                            locanReversePath.Add(localNeighbour);

                            break;
                        }
                    }
                }

                Cell[] neighbours = GetCellNeighbours(cell, allCells.ToArray());

                foreach (Cell neighbour in neighbours)
                {
                    if (neighbour.IsVisited || !neighbour.IsPassable)
                        continue;

                    neighbour.IsVisited = true;
                    neighbour.Distance = cell.Distance + 1;
                    queue.Enqueue(neighbour);
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
                if (TryGetCell(fieldList, neighbourCoords, out Cell neighbourCell) && !neighbourCell.IsVisited && neighbourCell.IsPassable)
                    neighbours.Add(neighbourCell);

            return neighbours.ToArray();
        }

        private class Cell
        {
            public Vector2Int Coordinates;
            public bool IsVisited;
            public int Distance;
            public bool IsPassable;

            public Cell(Vector2Int coordinates, bool isPassable)
            {
                Coordinates = coordinates;
                IsPassable = isPassable;
            }
        }
    }
}