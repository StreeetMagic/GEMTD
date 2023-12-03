using Gameplay.Fields.WallPlacers;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class PlaceWallsState : IState
    {
        private WallPlacer _wallPlacer = new WallPlacer();
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        public PlaceWallsState(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
        }

        public void Enter()
        {
            Debug.Log(" Entered PlaceWallsState");
        }

        public void Exit()
        {
            _wallPlacer.PlaceWalls();
            Debug.Log("Exited PlaceWallsState");
        }

        public void FinishPlacingWalls()
        {
            _gameLoopStateMachine.Enter<EnemyMoveState>();
        }
    }
}