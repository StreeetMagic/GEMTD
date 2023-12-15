using System;
using Gameplay.Fields.Walls.WallPlacers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.Services.CurrentDataServices;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class PlaceWallsState : IGameLoopStateMachineState
    {
        private readonly TowerPlacer _towerPlacer = new();
        private readonly IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        public PlaceWallsState(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
        }

        public event Action<IState> Entered;

        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public void Enter()
        {
            Entered?.Invoke(this);
        }

        public void Exit()
        {
            CurrentDataService.FieldModel.RoundNumber++;
        }

        public void PlaceWalls()
        {
            _towerPlacer.PlaceTowers(onComplete: () => { _gameLoopStateMachine.Enter<EnemyMoveState>(); });
        }
    }
}