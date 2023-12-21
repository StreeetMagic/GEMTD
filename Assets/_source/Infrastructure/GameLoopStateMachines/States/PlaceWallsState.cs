using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Towers;
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

        public async void Enter()
        {
            CurrentDataService.FieldModel.RoundNumber++;
            Entered?.Invoke(this);

            await PlaceTowers(CurrentDataService.PlayerModel.TowerTypes(out List<int> towerTypes), towerTypes);
        }

        public void Exit()
        {
        }

        private async UniTask PlaceTowers(List<TowerType> towerTypes, List<int> levels)
        {
            await _towerPlacer.PlaceTowers(towerTypes, levels);
            _gameLoopStateMachine.Enter<ChooseTowerState>();
        }
    }
}