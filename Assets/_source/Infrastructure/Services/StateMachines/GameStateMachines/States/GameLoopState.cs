using Debugs.Debug_HeadsUpDisplays;
using Gameplay.Fields.Walls.WallPlacers;
using Gameplay.Players;
using Games;
using Infrastructure.Services.AssetProviders;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.GameFactories;
using Infrastructure.Services.StateMachines.GameLoopStateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StaticDataServices;
using Infrastructure.Services.ZenjectFactory;
using UnityEngine;

namespace Infrastructure.Services.StateMachines.GameStateMachines.States
{
  public class GameLoopState : IGameState
  {
    private readonly IStateMachine<IGameLoopState> _gameLoopStateMachine;
    private readonly IGameFactoryService _gameFactoryService;
    private readonly ICurrentDataService _currentDataService;
    private readonly IZenjectFactory _zenjectFactory;
    private readonly IStaticDataService _staticDataService;

    public GameLoopState(IStateMachine<IGameLoopState> gameLoopStateMachine, IGameFactoryService gameFactoryService,
      ICurrentDataService currentDataService, IZenjectFactory zenjectFactory, IStaticDataService staticDataService)
    {
      _gameLoopStateMachine = gameLoopStateMachine;
      _gameFactoryService = gameFactoryService;
      _currentDataService = currentDataService;
      _zenjectFactory = zenjectFactory;
      _staticDataService = staticDataService;
    }

    public void Enter()
    {
      _currentDataService
        .PlayerModel = new PlayerModel();

      _currentDataService.FieldModel = _gameFactoryService.FieldFactory.CreateFieldModel(_staticDataService.GameConfig);
      _currentDataService.ThroneModel = _gameFactoryService.FieldFactory.CreateThroneModel();
      _gameFactoryService.FieldFactory.CreateFieldView(_currentDataService.FieldModel);
      _gameFactoryService.FieldFactory.CreateCheckpointsModels();
      _gameFactoryService.FieldFactory.CreateStartingLabyrinth();
      _gameFactoryService.CreateThrone(new Vector3(15, 0, 1));
      _zenjectFactory.Instantiate<DebugHeadsUpDisplay>();
      _gameLoopStateMachine.Enter<PlaceWallsState>();
    }

    public void Exit()
    {
    }
  }
}