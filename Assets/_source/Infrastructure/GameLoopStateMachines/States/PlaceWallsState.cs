using System;
using Gameplay.Fields.Cells;
using Gameplay.Fields.WallPlacers;
using Gameplay.Fields.Walls;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using InfastuctureCore.Services.StaticDataServices;
using Infrastructure.GameStateMachines;
using Infrastructure.Services.CurrentDataServices;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class PlaceWallsState : IState
    {
        private TowerPlacer _towerPlacer = new TowerPlacer();
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;
        private Coroutine _coroutine;

        public PlaceWallsState(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
        }

        public event Action<IState> Entered;
        public event Action<IExitableState> Exited;

        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<IStateMachineService<GameStateMachineData>>().Data.CoroutineRunner;
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();

        public void Enter()
        {
            Debug.Log("Я ИНВОКАЮ");
            Entered?.Invoke(this);
            Debug.Log(" Entered PlaceWallsState");
        }

        public void Exit()
        {
            Exited?.Invoke(this);
            Debug.Log("Exited PlaceWallsState");

            if (_coroutine != null)
            {
                CoroutineRunner.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        public void PlaceWalls()
        {
            _coroutine = CoroutineRunner.StartCoroutine(_towerPlacer.PlaceTowers(onComplete: () => { _gameLoopStateMachine.Enter<EnemyMoveState>(); }));
        }
    }
}