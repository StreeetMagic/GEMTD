﻿using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.Services.CurrentDataServices;

namespace Gameplay.Fields.WallPlacers
{
    public class WallPlacer
    {
        private int _roundNumber;

        private WallPlacerConfig WallPlacerConfig => ServiceLocator.Instance.Get<IStaticDataService>().Get<WallPlacerConfig>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public void PlaceWalls()
        {
            if (WallPlacerConfig.WallSettingsPerRounds[_roundNumber].DestroyList.Count > 0)
            {
                foreach (var coordinates in WallPlacerConfig.WallSettingsPerRounds[_roundNumber].DestroyList)
                {
                    RemoveWall(coordinates);
                }
            }

            foreach (var coordinates in WallPlacerConfig.WallSettingsPerRounds[_roundNumber].PlaceList)
            {
                AddWall(coordinates);
            }

            _roundNumber++;
        }

        private void AddWall(Coordinates coordinates)
        {
            CurrentDataService.FieldData.GetCellDataByCoordinates(coordinates).SetWallData(new WallData());
        }

        private void RemoveWall(Coordinates coordinates)
        {
            CurrentDataService.FieldData.GetCellDataByCoordinates(coordinates).RemoveWallData(); 
        }
    }
}