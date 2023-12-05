using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;
using UnityEngine;

namespace Gameplay.Fields
{
    public class FieldData
    {
        private readonly CellData[] _cellDatas;

        public FieldData(CellData[] cellDatas)
        {
            _cellDatas = cellDatas;
        }

        public int RoundNumber { get; set; } = 1;
        public CellData[] CellDatas => _cellDatas.ToArray();
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public CellData GetCellData(Coordinates coordinates)
        {
            foreach (CellData cellData in _cellDatas)
            {
                if (cellData.Coordinates.Equals(coordinates))
                {
                    return cellData;
                }
            }

            return null;
        }

        public Coordinates[] GetCentralWalls(int towerPerRound)
        {
            int size = StaticDataService.Get<FieldConfig>().FieldSize;

            int centralCoordinate = size / 2 + 1;

            List<Coordinates> coordinates = new List<Coordinates>();

            while (coordinates.Count < towerPerRound)
            {
                for (int i = 0; i < centralCoordinate; i++)
                {
                    for (int x = centralCoordinate - i; x < centralCoordinate + i; x++)
                    {
                        for (int z = centralCoordinate - i; z < centralCoordinate + i; z++)
                        {
                            CellData cellData = GetCellData(new Coordinates(x, z));

                            if (cellData.CanBeReplacedWithTower)
                            {
                                coordinates.Add(new Coordinates(x, z));
                            }

                            if (coordinates.Count == towerPerRound)
                            {
                                if (HasSameCoordinates(coordinates.ToArray()))
                                {
                                    coordinates.Clear();
                                }
                                else
                                {
                                    return coordinates.ToArray();
                                }
                            }
                        }
                    }
                }
            }

            return coordinates.ToArray();
        }

        private bool HasSameCoordinates(Coordinates[] coordinates)
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                for (int j = i + 1; j < coordinates.Length; j++)
                {
                    if (coordinates[i].Equals(coordinates[j]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}