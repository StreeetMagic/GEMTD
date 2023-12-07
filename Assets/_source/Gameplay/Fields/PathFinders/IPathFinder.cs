using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.PathFinders
{
    public interface IPathFinder
    {
        void FindPath(CellModel[] field, CoordinatesValues startCoordinatesValues, CoordinatesValues finishCoordinates, List<CoordinatesValues> path);
    }

    public class BreadthFirstPathFinder : IPathFinder
    {
        public void FindPath(CellModel[] field, CoordinatesValues startCoordinatesValues, CoordinatesValues finishCoordinates, List<CoordinatesValues> path)
        {
            Debug.Log("Строю маршрут от " + startCoordinatesValues.X + " " + startCoordinatesValues.Z + " до " + finishCoordinates.X + " " + finishCoordinates.Z);

            List<Cell> allCells = new List<Cell>();

            foreach (CellModel cell in field)
                allCells.Add(new Cell(cell.CoordinatesValues, cell.IsPassable));

            TryGetCell(allCells, startCoordinatesValues, out Cell startCell);
            startCell.IsStart = true;

            TryGetCell(allCells, finishCoordinates, out Cell finishCell);
            finishCell.IsFinish = true;

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

                        if (neighbour.CoordinatesValues.X == finishCoordinates.X && neighbour.CoordinatesValues.Z == finishCoordinates.Z)
                        {
                            currentCell = neighbour;

                            List<CoordinatesValues> localPath = new List<CoordinatesValues>();

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

        private bool TryGetCell(List<Cell> allCells, CoordinatesValues coordinatesValues, out Cell thisCell)
        {
            foreach (Cell cell in allCells)
            {
                if (cell.CoordinatesValues.X == coordinatesValues.X && cell.CoordinatesValues.Z == coordinatesValues.Z)
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
            CoordinatesValues cellCoordinates = cell.CoordinatesValues;

            List<Cell> neighbours = new List<Cell>();

            if (TryGetCell(fieldList, new CoordinatesValues(cellCoordinates.X - 1, cellCoordinates.Z), out Cell leftCell) && leftCell.IsVisited == false && leftCell.IsPassable)
                neighbours.Add(leftCell);

            if (TryGetCell(fieldList, new CoordinatesValues(cellCoordinates.X + 1, cellCoordinates.Z), out Cell rightCell) && rightCell.IsVisited == false && rightCell.IsPassable)
                neighbours.Add(rightCell);

            if (TryGetCell(fieldList, new CoordinatesValues(cellCoordinates.X, cellCoordinates.Z - 1), out Cell downCell) && downCell.IsVisited == false && downCell.IsPassable)
                neighbours.Add(downCell);

            if (TryGetCell(fieldList, new CoordinatesValues(cellCoordinates.X, cellCoordinates.Z + 1), out Cell upCell) && upCell.IsVisited == false && upCell.IsPassable)
                neighbours.Add(upCell);

            return neighbours.ToArray();
        }

        private class Cell
        {
            public CoordinatesValues CoordinatesValues;
            public bool IsFinish;
            public bool IsStart;
            public bool IsVisited;
            public int Distance;
            public bool IsPassable;

            public Cell(CoordinatesValues coordinatesValues, bool isPassable)
            {
                CoordinatesValues = coordinatesValues;
                IsPassable = isPassable;
            }
        }
    }
}