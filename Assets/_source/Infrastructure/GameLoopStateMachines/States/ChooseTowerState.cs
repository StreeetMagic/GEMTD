using System;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls.WallPlacers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.Services.GameFactoryServices;
using UserInterface;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class ChooseTowerState : IGameLoopStateMachineState
    {
        private readonly IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;
        private readonly TowerPlacer _towerPlacer;
        private readonly HeadsUpDisplay _headsUpDisplay;

        public ChooseTowerState(TowerPlacer towerPlacer, IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _towerPlacer = towerPlacer;
            _gameLoopStateMachine = gameLoopStateMachine;
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

        public async UniTask ConfirmTower(CellModel cellModel)
        {
            await _towerPlacer.ConfirmTower(cellModel);
            _gameLoopStateMachine.Enter<EnemyMoveState>();
        }
    }
}