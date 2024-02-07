using Gameplay.Fields;
using Gameplay.Fields.Checkpoints;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.Labytinths;
using Gameplay.Fields.Towers;
using Gameplay.Fields.Walls.WallPlacers;
using Games;
using Infrastructure.DIC;
using Infrastructure.Services.CoroutineRunners;
using Infrastructure.Services.StateMachines.GameLoopStateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachines.StateFactories;
using UnityEngine;
using UnityEngine.SceneManagement;
using IStaticDataService = Infrastructure.Services.StaticDataServices.IStaticDataService;

namespace Infrastructure.Services.StateMachines.GameStateMachines.States
{
  public class BootstrapState : IGameState
  {
    private readonly IStateMachine<IGameState> _gameStateMachine;
    private readonly IStateMachine<IGameLoopState> _gameLoopStateMachine;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IStateFactory _stateFactory;
    private readonly IStaticDataService _staticDataService;
    private readonly IGodFactory _godFactory;

    public BootstrapState(IStateMachine<IGameState> gameStateMachine, IStateMachine<IGameLoopState> gameLoopStateMachine,
      ICoroutineRunner coroutineRunner, IStaticDataService staticDataService, IGodFactory godFactory,
      IStateFactory stateFactory)
    {
      _gameStateMachine = gameStateMachine;
      _coroutineRunner = coroutineRunner;
      _staticDataService = staticDataService;
      _godFactory = godFactory;
      _stateFactory = stateFactory;
      _gameLoopStateMachine = gameLoopStateMachine;
    }

    public void Enter()
    {
      RegisterGameLoopStates();

      RegisterConfigs();
      EnterNextState();
    }

    public void Exit()
    {
    }

    private void RegisterConfigs()
    {
      _staticDataService.RegisterConfigs();
      

    }

    private void RegisterGameLoopStates()
    {
      _gameLoopStateMachine.Register(_stateFactory.Create<PlaceWallsState>());
      _gameLoopStateMachine.Register(_stateFactory.Create<ChooseTowerState>());
      _gameLoopStateMachine.Register(_stateFactory.Create<EnemyMoveState>());
      _gameLoopStateMachine.Register(_stateFactory.Create<WinState>());
      _gameLoopStateMachine.Register(_stateFactory.Create<LoseState>());
    }

    private void EnterNextState() =>
      _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name == Constants.Scenes.InitialScene
        ? Constants.Scenes.Gameloop
        : SceneManager.GetActiveScene().name);
  }
}