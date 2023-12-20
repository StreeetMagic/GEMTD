using System;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls.WallPlacers;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
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

        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void Enter()
        {
            Entered?.Invoke(this);
            _headsUpDisplay.ChooseTowerPanel.gameObject.SetActive(true);
            _headsUpDisplay.ChooseTowerPanel.OnChooseTowerStateEntered();
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