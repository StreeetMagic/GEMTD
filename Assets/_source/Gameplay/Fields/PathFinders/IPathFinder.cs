using System.Collections.Generic;
using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.PathFinders
{
    public interface IPathFinder
    {
        CoordinatesValues[] FindPath(CellModel[] field, CoordinatesValues startCoordinatesValues, CoordinatesValues finishCoordinates);
    }

    public class BreadthFirstPathFinder : IPathFinder
    {
        public CoordinatesValues[] FindPath(CellModel[] field, CoordinatesValues startCoordinatesValues, CoordinatesValues finishCoordinates)
        {
            Debug.Log("Строю маршрут от " + startCoordinatesValues.X + " " + startCoordinatesValues.Z + " до " + finishCoordinates.X + " " + finishCoordinates.Z);

            List<CoordinatesValues> path = new List<CoordinatesValues>();

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

                            path.Add(currentCell.CoordinatesValues);

                            int finishDistance = currentCell.Distance;

                            while (finishDistance > 0)
                            {
                                finishDistance--;

                                Cell[] newNeighbours = GetCellNeighbours(currentCell, allCells.ToArray());

                                Cell pathCell = null;

                                foreach (Cell newNeighbour in newNeighbours)
                                {
                                    if (newNeighbour.Distance == finishDistance)
                                    {
                                        pathCell = newNeighbour;

                                        path.Add(pathCell.CoordinatesValues);

                                        if (pathCell.CoordinatesValues.X == startCoordinatesValues.X && pathCell.CoordinatesValues.Z == startCoordinatesValues.Z)
                                        {
                                            return path.ToArray();
                                        }

                                        break; // Exit the loop after finding the correct path cell
                                    }
                                }

                                if (pathCell == null)
                                {
                                    // If no path cell is found, break the loop
                                    break;
                                }

                                currentCell = pathCell; // Move to the next cell
                            }
                        }
                    }
                }
            }

            return path.ToArray();
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