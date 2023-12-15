using System.Collections.Generic;
using Gameplay.Fields.Cells;
using UnityEngine;

namespace Gameplay.Fields.PathFinders
{
    public interface IPathFinder
    {
        void FindPath(CellModel[] field, Vector2Int startCoordinatesValues, Vector2Int finishCoordinates, List<Vector2Int> path);
    }

    public class CheckPointPathFinder : IPathFinder
    {
        public void FindPath(CellModel[] field, Vector2Int startCoordinatesValues, Vector2Int finishCoordinates, List<Vector2Int> path)
        {
            path.Add(startCoordinatesValues);
            path.Add(finishCoordinates);
        }
    }

    public class BreadthFirstPathFinder : IPathFinder
    {
        public void FindPath(CellModel[] field, Vector2Int startCoordinatesValues, Vector2Int finishCoordinates, List<Vector2Int> path)
        {
            List<GameObject> debugTexts = new List<GameObject>();

            List<Cell> allCells = new List<Cell>();

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

            startCell.Distance = 0;
            startCell.IsVisited = true;

            Queue<Cell> queue = new Queue<Cell>();

            Debug.LogWarning("Стартовая клетка координаты " + startCell.Coordinates);
            queue.Enqueue(startCell);

            var fdt = Object.Instantiate(Resources.Load<CellDebugText>("CellDebugText"), new Vector3(startCell.Coordinates.x, 0, startCell.Coordinates.y), Quaternion.identity);
            fdt.Text.text = "(" + startCell.Coordinates.x + ", " + startCell.Coordinates.y + ")" + "\n " + startCell.Distance;
            debugTexts.Add(fdt.gameObject);

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

                    Debug.LogWarning("Мы нашли конец и закончили поиск пути");
                    var finishCellText = Object.Instantiate(Resources.Load<CellDebugText>("CellDebugText"), new Vector3(cell.Coordinates.x, 0, cell.Coordinates.y), Quaternion.identity);
                    finishCellText.Text.text = "(" + cell.Coordinates.x + ", " + cell.Coordinates.y + ")" + "\n " + cell.Distance;
                    debugTexts.Add(fdt.gameObject);

                    int distance = cell.Distance;

                    List<Cell> locanReversePath = new List<Cell>();
                    locanReversePath.Add(finishCell);

                    while (true)
                    {
                        Debug.LogWarning("Начало форич");

                        foreach (var pathik in locanReversePath)
                            Debug.LogWarning(pathik.Coordinates);

                        Debug.LogWarning("Конец форич");

                        Cell[] localNeighbours = GetCellNeighbours(cell, allCells.ToArray());

                        Debug.LogWarning(localNeighbours.Length);

                        CellDebugText localCellText = null;

                        foreach (Cell localNeighbour in localNeighbours)
                        {
                            if (localNeighbour.Distance == distance - 1)
                            {
                                if (localNeighbour.Distance == 0)
                                {
                                    Debug.LogWarning("КОНЕЦ");

                                    locanReversePath.Add(localNeighbour);

                                    locanReversePath.Reverse();

                                    foreach (GameObject variable in debugTexts)
                                    {
                                        Object.Destroy(variable);
                                    }

                                    foreach (Cell localCell in locanReversePath)
                                    {
                                        path.Add(localCell.Coordinates);
                                    }

                                    localCellText = Object.Instantiate(Resources.Load<CellDebugText>("CellDebugText"), new Vector3(localNeighbour.Coordinates.x, 0, localNeighbour.Coordinates.y), Quaternion.identity);
                                    localCellText.Text.text = "(" + localNeighbour.Coordinates.x + ", " + localNeighbour.Coordinates.y + ")" + "\n " + localNeighbour.Distance;
                                    debugTexts.Add(fdt.gameObject);

                                    foreach (var debugText in debugTexts)
                                    {
                                        Object.Destroy(debugText);
                                    }

                                    return;
                                }

                                localCellText = Object.Instantiate(Resources.Load<CellDebugText>("CellDebugText"), new Vector3(localNeighbour.Coordinates.x, 0, localNeighbour.Coordinates.y), Quaternion.identity);
                                localCellText.Text.text = "(" + localNeighbour.Coordinates.x + ", " + localNeighbour.Coordinates.y + ")" + "\n " + localNeighbour.Distance;
                                debugTexts.Add(fdt.gameObject);

                                distance--;
                                cell = localNeighbour;
                                locanReversePath.Add(localNeighbour);

                                break;
                            }
                        }
                    }
                }

                Cell[] neighbours = GetCellNeighbours(cell, allCells.ToArray());

                foreach (Cell neighbour in neighbours)
                {
                    if (neighbour.IsVisited == false && neighbour.IsPassable)
                    {
                        neighbour.IsVisited = true;
                        neighbour.Distance = cell.Distance + 1;
                        queue.Enqueue(neighbour);

                        var cdt = Object.Instantiate(Resources.Load<CellDebugText>("CellDebugText"), new Vector3(neighbour.Coordinates.x, 0, neighbour.Coordinates.y), Quaternion.identity);
                        cdt.Text.text = "(" + neighbour.Coordinates.x + ", " + neighbour.Coordinates.y + ")" + "\n " + neighbour.Distance;
                        debugTexts.Add(cdt.gameObject);
                    }
                }
            }
        }

        private bool TryGetCell(List<Cell> allCells, Vector2Int coordinatesValues, out Cell thisCell)
        {
            foreach (Cell cell in allCells)
            {
                if (cell.Coordinates.x == coordinatesValues.x && cell.Coordinates.y == coordinatesValues.y)
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
            Vector2Int cellCoordinates = cell.Coordinates;

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