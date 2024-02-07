﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Walls.WallPlacers;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.StateMachines.States;

namespace Infrastructure.Services.StateMachines.GameLoopStateMachines.States
{
  public class PlaceWallsState : IGameLoopState
  {
    private readonly TowerPlacer _towerPlacer;
    private readonly IStateMachine<IGameLoopState> _gameLoopStateMachine;
    private readonly ICurrentDataService _currentDataService;

    public PlaceWallsState(IStateMachine<IGameLoopState> gameLoopStateMachine, TowerPlacer towerPlacer, ICurrentDataService currentDataService)
    {
      _gameLoopStateMachine = gameLoopStateMachine;
      _towerPlacer = towerPlacer;
      _currentDataService = currentDataService;
    }

    public event Action<IState> Entered;

    public async void Enter()
    {
      _currentDataService.FieldModel.RoundNumber++;
      Entered?.Invoke(this);

      await PlaceTowers(_currentDataService.PlayerModel.TowerTypes(out List<int> levels), levels);
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