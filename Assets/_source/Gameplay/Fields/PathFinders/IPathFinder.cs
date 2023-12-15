using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.PathFinders
{
    public interface IPathFinder
    {
        void FindPath(CellModel[] field, Vector2Int startCoordinatesValues, Vector2Int finishCoordinates, List<Vector2Int> path);
    }

    public class BreadthFirstPathFinder : IPathFinder
    {
        public void FindPath(CellModel[] field, Vector2Int startCoordinatesValues, Vector2Int finishCoordinates, List<Vector2Int> path)
        {
            Debug.Log("Строю маршрут от " + startCoordinatesValues.x + " " + startCoordinatesValues.y + " до " + finishCoordinates.x + " " + finishCoordinates.y);

            List<Cell> allCells = new List<Cell>();

            foreach (CellModel cell in field)
                allCells.Add(new Cell(cell.CoordinatesValues, cell.IsPassable));

            TryGetCell(allCells, startCoordinatesValues, out Cell startCell);

            TryGetCell(allCells, finishCoordinates, out Cell finishCell);

            Queue<Cell> queue = new Queue<Cell>();

            int distance = 0;
            startCell.Distance = distance;

            queue.Enqueue(startCell);

            while (queue.Count > 0)
            {
                Cell currentCell = queue.Dequeue();
                distance = currentCell.Distance + 1;
                currentCell.IsVisited = true;

                Cell[] neighbours = GetCellNeighbours(currentCell, allCells.ToArray());

                foreach (Cell neighbour in neighbours)
                {
                    if (!neighbour.IsVisited)
                    {
                        neighbour.IsVisited = true;
                        neighbour.Distance = distance;

                        queue.Enqueue(neighbour);

                        if (neighbour.CoordinatesValues.x == finishCoordinates.x && neighbour.CoordinatesValues.y == finishCoordinates.y)
                        {
                            currentCell = neighbour;

                            List<Vector2Int> localPath = new List<Vector2Int>();

                            localPath.Add(currentCell.CoordinatesValues);

                            int finishDistance = currentCell.Distance;

                            Debug.Log("Определили конец: " + finishDistance);

                            while (finishDistance > 0)
                            {
                                finishDistance--;

                                Debug.Log("Distance: " + finishDistance);

                                Cell[] newNeighbours = GetCellNeighbours(currentCell, allCells.ToArray());

                                foreach (Cell newNeighbour in newNeighbours)
                                {
                                    if (newNeighbour.Distance == 0)
                                    {
                                        Debug.Log("ДАААААА СУКААА");

                                        currentCell = newNeighbour;

                                        localPath.Add(currentCell.CoordinatesValues);

                                        localPath.Reverse();

                                        Debug.Log("Нашелся путь: " + localPath.Count);

                                        path.AddRange(localPath);

                                        break; // Exit the loop after finding the correct path cell
                                    }

                                    if (newNeighbour.Distance == finishDistance)
                                    {
                                        currentCell = newNeighbour;
                                        localPath.Add(currentCell.CoordinatesValues);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool TryGetCell(List<Cell> allCells, Vector2Int coordinatesValues, out Cell thisCell)
        {
            foreach (Cell cell in allCells)
            {
                if (cell.CoordinatesValues.x == coordinatesValues.x && cell.CoordinatesValues.y == coordinatesValues.y)
                {
                    thisCell = cell;
                    return true;
                }
            }

            thisCell = null;
            return false;
        }

        private Cell[] GetCellNeighbours(Cell cell, Cell[] field)
        {
            List<Cell> fieldList = new List<Cell>(field);
            Vector2Int cellCoordinates = cell.CoordinatesValues;

            List<Cell> neighbours = new List<Cell>();

            if (TryGetCell(fieldList, new Vector2Int(cellCoordinates.x - 1, cellCoordinates.y), out Cell leftCell) && leftCell.IsVisited == false && leftCell.IsPassable)
                neighbours.Add(leftCell);

            if (TryGetCell(fieldList, new Vector2Int(cellCoordinates.x + 1, cellCoordinates.y), out Cell rightCell) && rightCell.IsVisited == false && rightCell.IsPassable)
                neighbours.Add(rightCell);

            if (TryGetCell(fieldList, new Vector2Int(cellCoordinates.x, cellCoordinates.y - 1), out Cell downCell) && downCell.IsVisited == false && downCell.IsPassable)
                neighbours.Add(downCell);

            if (TryGetCell(fieldList, new Vector2Int(cellCoordinates.x, cellCoordinates.y + 1), out Cell upCell) && upCell.IsVisited == false && upCell.IsPassable)
                neighbours.Add(upCell);

            return neighbours.ToArray();
        }

        private class Cell
        {
            public Vector2Int CoordinatesValues;
            public bool IsVisited;
            public int Distance;
            public bool IsPassable;

            public Cell(Vector2Int coordinatesValues, bool isPassable)
            {
                CoordinatesValues = coordinatesValues;
                IsPassable = isPassable;
            }
        }
    }
}