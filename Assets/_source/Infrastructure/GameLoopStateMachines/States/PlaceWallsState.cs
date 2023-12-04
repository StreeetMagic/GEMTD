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
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

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

            // if (_coroutine != null)
            // {
            //     CoroutineRunner.StopCoroutine(_coroutine);
            //     _coroutine = null;
            // }
        }

        public void PlaceWalls()
        {
            Coordinates[] placedTowers = new Coordinates[StaticDataService.Get<WallPlacerConfig>().towerPerRound];

            _coroutine = CoroutineRunner.StartCoroutine(_towerPlacer.PlaceTowers(placedTowers, onComplete: (hasTowers) =>
            {
                if (hasTowers)
                    ChooseRandomTower(placedTowers);

                _gameLoopStateMachine.Enter<EnemyMoveState>();
            }));
        }

        private void ChooseRandomTower(Coordinates[] placedTowers)
        {
            int randomIndex = Random.Range(0, placedTowers.Length);

            for (int i = 0; i < placedTowers.Length; i++)
            {
                if (i == randomIndex)
                    continue;

                CurrentDataService.FieldData.GetCellData(placedTowers[i]).ReplaceTowerWithWall(GameFactory.BlockGridFactory.CreateWallData());
            }
        }
    }
}