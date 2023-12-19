using System;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls.WallPlacers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.CoroutineRunnerServices;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.Services.GameFactoryServices;
using UserInterface;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class ChooseTowerState : IGameLoopStateMachineState
    {
        private readonly IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;
        private TowerPlacer _towerPlacer;
        private HeadUpDisplay _headUpDisplay;

        public ChooseTowerState(TowerPlacer towerPlacer, IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _towerPlacer = towerPlacer;
            _gameLoopStateMachine = gameLoopStateMachine;
            _headUpDisplay = GameFactory.UserInterfaceFactory.CreateHeadUpDisplay();
            _headUpDisplay.gameObject.SetActive(false);
        }

        public event Action<IGameLoopStateMachineState> Entered;

        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();
        private ICoroutineRunnerService CoroutineRunner => ServiceLocator.Instance.Get<ICoroutineRunnerService>();

        public void Enter()
        {
            Entered?.Invoke(this);
            _headUpDisplay.gameObject.SetActive(true);
            _headUpDisplay.OnChooseTowerStateEntered();
        }

        public void Exit()
        {
            _headUpDisplay.gameObject.SetActive(false);
        }

        public async UniTask ConfirmTower(CellModel cellModel)
        {
            await _towerPlacer.ConfirmTower(cellModel);
            _gameLoopStateMachine.Enter<EnemyMoveState>();
        }
    }
}