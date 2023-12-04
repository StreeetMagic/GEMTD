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

namespace Infrastructure.GameLoopStateMachines.States
{
    public class PlaceWallsState : IState
    {
        private TowerPlacer _towerPlacer = new TowerPlacer();
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        public PlaceWallsState(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
        }

        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<IStateMachineService<GameStateMachineData>>().Data.CoroutineRunner;
        private IStaticDataService StaticDataService => ServiceLocator.Instance.Get<IStaticDataService>();
        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();
        private IGameFactoryService GameFactory => ServiceLocator.Instance.Get<IGameFactoryService>();

        public void Enter()
        {
            Debug.Log(" Entered PlaceWallsState");
        }

        public void Exit()
        {
            Coordinates[] placedTowers = new Coordinates[StaticDataService.Get<WallPlacerConfig>().towerPerRound];

            CoroutineRunner.StartCoroutine(_towerPlacer.PlaceTowers(placedTowers, onComplete: () =>
            {
                ChooseRandomTower(placedTowers);
                Debug.Log("Exited PlaceWallsState");
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

        public void FinishPlacingTowers()
        {
            _gameLoopStateMachine.Enter<EnemyMoveState>();
        }
    }
}