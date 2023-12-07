using System.Collections.Generic;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Cells.CellsContainers;
using Gameplay.Fields.EnemySpawners;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;

namespace Gameplay.Fields
{
    public class FieldModel
    {
        public FieldModel(CellModel[] cellmodels, EnemySpawnerModel enemyEnemySpawnerModel)
        {
            EnemySpawnerModel = enemyEnemySpawnerModel;
            CellsContainerModel = new CellsContainerModel(cellmodels); 
        }

        public CellsContainerModel CellsContainerModel { get; }
        public EnemySpawnerModel EnemySpawnerModel { get; }
        public int RoundNumber { get; set; } = 1;

        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public CoordinatesValues[] GetCentralWalls(int towerPerRound)
        {
            int size = StaticDataService.Get<FieldConfig>().FieldSize;
            int centralCoordinate = size / 2 + 1;
            List<CoordinatesValues> coordinates = new List<CoordinatesValues>();

            while (coordinates.Count < towerPerRound)
            {
                for (int i = 0; i < centralCoordinate; i++)
                {
                    FindValidCoordinates(centralCoordinate, i, coordinates, towerPerRound);

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

            return coordinates.ToArray();
        }

        private void FindValidCoordinates(int centralCoordinate, int i, List<CoordinatesValues> coordinates, int towerPerRound)
        {
            for (int x = centralCoordinate - i; x < centralCoordinate + i; x++)
            {
                for (int z = centralCoordinate - i; z < centralCoordinate + i; z++)
                {
                    CellModel cellModel = CellsContainerModel.GetCellModel(new CoordinatesValues(x, z));

                    if (cellModel.CanBeReplacedWithTower)
                    {
                        coordinates.Add(new CoordinatesValues(x, z));
                    }

                    if (coordinates.Count == towerPerRound)
                    {
                        return;
                    }
                }
            }
        }

        private bool HasSameCoordinates(CoordinatesValues[] coordinates)
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