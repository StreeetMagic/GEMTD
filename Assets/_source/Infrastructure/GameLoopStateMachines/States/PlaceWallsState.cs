using System;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Walls.WallPlacers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.Services.CurrentDataServices;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class PlaceWallsState : IGameLoopStateMachineState
    {
        private readonly TowerPlacer _towerPlacer;
        private readonly IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        public PlaceWallsState(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine, TowerPlacer towerPlacer)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
            _towerPlacer = towerPlacer;
        }

        public event Action<IState> Entered;

        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public void Enter()
        {
            CurrentDataService.FieldModel.RoundNumber++;
            Entered?.Invoke(this);

            UniTask asd = PlaceWalls();
        }

        public void Exit()
        {
        }

        private async UniTask PlaceWalls()
        {
            await _towerPlacer.PlaceTowers();
            _gameLoopStateMachine.Enter<ChooseTowerState>();
        }
    }
}