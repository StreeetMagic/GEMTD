using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Towers.Resources;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Fields
{
    public class FieldData
    {
        private readonly CellData[] _cellDatas;

        public FieldData(CellData[] cellDatas)
        {
            _cellDatas = cellDatas;
        }

        public CellData[] CellDatas => _cellDatas.ToArray();
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

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

        public Coordinates[] GetCentalWalls(int towerPerRound)
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
                            Debug.Log("Координаты в цикле " + x + " " + z);

                            CellData cellData = GetCellData(new Coordinates(x, z));

                            if (cellData.WallData != null)
                            {
                                if (cellData.TowerData == null)
                                    coordinates.Add(new Coordinates(x, z));
                            }

                            if (coordinates.Count == towerPerRound)
                                return coordinates.ToArray();
                        }
                    }
                }
            }

            return coordinates.ToArray();
        }

        private void TrySetTower(CellData cellData, List<Coordinates> coordinates, int i, int j)
        {
            if (cellData.WallData != null)
            {
                if (cellData.TowerData == null)
                {
                    cellData.RemoveWallData();
                    cellData.SetTowerData(GameFactory.BlockGridFactory.CreateTowerData((TowerType)Random.Range(0, 8), 1));

                    coordinates.Add(new Coordinates(i, j));
                }
            }
        }
    }
}