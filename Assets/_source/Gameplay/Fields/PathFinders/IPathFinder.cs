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
            List<CoordinatesValues> path = new List<CoordinatesValues>();

            List<Cell> allCells = new List<Cell>();

            foreach (CellModel cell in field)
                allCells.Add(new Cell(cell.CoordinatesValues));

            TryGetCell(allCells, startCoordinatesValues, out Cell startCell);
            startCell.IsStart = true;

            TryGetCell(allCells, finishCoordinates, out Cell finishCell);
            finishCell.IsFinish = true;

            Queue<Cell> queue = new Queue<Cell>();

            int distance = 0;
            startCell.Distance = distance;

            queue.Enqueue(startCell);

            while (true)
            {
                distance++;

                Cell currentCell = queue.Dequeue();
                currentCell.IsVisited = true;

                if (currentCell.CoordinatesValues.X == finishCoordinates.X && currentCell.CoordinatesValues.Z == finishCoordinates.Z)
                {
                    path.Add(currentCell.CoordinatesValues);

                    int finishDistance = currentCell.Distance;

                    while (true)
                    {
                        finishDistance--;

                        Cell[] newNeighbours = GetCellNeighbours(currentCell, allCells.ToArray());

                        Cell pathCell;

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
                            }
                        }
                    }
                }

                Cell[] neighbours = GetCellNeighbours(currentCell, allCells.ToArray());

                foreach (Cell neighbour in neighbours)
                {
                    neighbour.IsVisited = true;
                    neighbour.Distance = distance;

                    queue.Enqueue(neighbour);
                    Debug.Log("Neighbour: " + neighbour.CoordinatesValues);
                }
            }

            return path.ToArray();
        }

        private CoordinatesValues[] DebugMethod()
        {
            List<CoordinatesValues> path = new List<CoordinatesValues>();

            for (int i = 0; i < 30; i++)
            {
                path.Add(new CoordinatesValues(i, i));
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

            if (TryGetCell(fieldList, new CoordinatesValues(cellCoordinates.X - 1, cellCoordinates.Z), out Cell leftCell) && leftCell.IsVisited == false)
                neighbours.Add(leftCell);

            if (TryGetCell(fieldList, new CoordinatesValues(cellCoordinates.X + 1, cellCoordinates.Z), out Cell rightCell) && rightCell.IsVisited == false)
                neighbours.Add(rightCell);

            if (TryGetCell(fieldList, new CoordinatesValues(cellCoordinates.X, cellCoordinates.Z - 1), out Cell downCell) && downCell.IsVisited == false)
                neighbours.Add(downCell);

            if (TryGetCell(fieldList, new CoordinatesValues(cellCoordinates.X, cellCoordinates.Z + 1), out Cell upCell) && upCell.IsVisited == false)
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

            public Cell(CoordinatesValues coordinatesValues)
            {
                CoordinatesValues = coordinatesValues;
            }
        }
    }
}