using System;
using System.Collections.Generic;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls.WallPlacers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using UserInterface;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class ChooseTowerState : IGameLoopStateMachineState
    {
        private readonly TowerPlacer _towerPlacer;
        private readonly HeadsUpDisplay _headsUpDisplay;

        public ChooseTowerState(TowerPlacer towerPlacer)
        {
            _towerPlacer = towerPlacer;
            _headsUpDisplay = GameFactory.UserInterfaceFactory.CreateHeadUpDisplay();
        }

        public event Action<IGameLoopStateMachineState> Entered;

        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public void Enter()
        {
            Entered?.Invoke(this);
            _headsUpDisplay.ChooseTowerPanel.gameObject.SetActive(true);

            int roundNumber = CurrentDataService.FieldModel.RoundNumber;
            WallSettingsPerRound[] towerIndexes = StaticDataService.Get<WallPlacerConfig>().WallSettingsPerRounds.ToArray();
            List<Vector2Int> wallsCoordinates = towerIndexes[roundNumber - 1].PlaceList;

            _headsUpDisplay.ChooseTowerPanel.OnChooseTowerStateEntered(wallsCoordinates);
        }

        public void Exit()
        {
        }

        public async void ConfirmTower(CellModel cellModel, Action onComplete = null)
        {
            await _towerPlacer.ConfirmTower(cellModel);
            onComplete?.Invoke();
        }
    }
}