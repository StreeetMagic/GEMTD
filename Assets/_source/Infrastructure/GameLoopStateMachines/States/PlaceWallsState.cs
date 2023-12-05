using System;
using Gameplay.Fields.WallPlacers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.GameStateMachines;
using Infrastructure.Services.CurrentDataServices;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class PlaceWallsState : IState
    {
        private readonly TowerPlacer _towerPlacer = new();
        private readonly IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        public PlaceWallsState(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
        }

        public event Action<IState> Entered;

        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<IStateMachineService<GameStateMachineData>>().Data.CoroutineRunner;
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public void Enter()
        {
            Entered?.Invoke(this);
            Debug.Log(" Entered PlaceWallsState");
        }

        public void Exit()
        {
            CurrentDataService.FieldData.RoundNumber++;
            Debug.Log("Exited PlaceWallsState");
        }

        public void PlaceWalls()
        {
            _towerPlacer.PlaceTowers(onComplete: () => { _gameLoopStateMachine.Enter<EnemyMoveState>(); });
        }
    }
}