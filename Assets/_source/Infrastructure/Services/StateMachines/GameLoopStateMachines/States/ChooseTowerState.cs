using System;
using System.Collections.Generic;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls.WallPlacers;
using Infrastructure.DIC;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.GameFactories;
using Infrastructure.Services.StaticDataServices;
using Infrastructure.Services.ZenjectFactory;
using UnityEngine;
using UserInterface.HeadsUpDisplays;

namespace Infrastructure.Services.StateMachines.GameLoopStateMachines.States
{
  public class ChooseTowerState : IGameLoopState
  {
    private readonly TowerPlacer _towerPlacer;
    private readonly ICurrentDataService _currentDataService;
    private readonly IStaticDataService _staticDataService;
    private readonly IZenjectFactory _zenjectFactory;

    private HeadsUpDisplayView _headsUpDisplayView;

    public ChooseTowerState(TowerPlacer towerPlacer, ICurrentDataService currentDataService,
      IGameFactoryService gameFactory, IStaticDataService staticDataService, IZenjectFactory zenjectFactory)
    {
      _towerPlacer = towerPlacer;
      _currentDataService = currentDataService;
      _staticDataService = staticDataService;
      _zenjectFactory = zenjectFactory;
    }

    public event Action<IGameLoopState> Entered;

    public void Enter()
    {
      Entered?.Invoke(this);

      _headsUpDisplayView = _zenjectFactory.Instantiate<HeadsUpDisplayView>((Transform)null);

      _headsUpDisplayView.ChooseTowerPanelView.gameObject.SetActive(true);

      int roundNumber = _currentDataService.FieldModel.RoundNumber;
      WallSettingsPerRound[] towerIndexes = _staticDataService.WallPlacerConfig.WallSettingsPerRounds.ToArray();
      List<Vector2Int> wallsCoordinates = towerIndexes[roundNumber - 1].PlaceList;

      _headsUpDisplayView.ChooseTowerPanelView.OnChooseTowerStateEntered(wallsCoordinates);
    }

    public void Exit()
    {
    }

    public async void ConfirmTower(CellModel cellModel, Action onComplete = null)
    {
      await _towerPlacer.ConfirmTower(cellModel);
      onComplete?.Invoke();
    }
  }
}