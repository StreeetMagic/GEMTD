using Games;
using Infrastructure.DIC;
using Infrastructure.Services.CoroutineRunners;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachines.StateFactories;
using Infrastructure.Services.StaticDataServices;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.StateMachines.GameStateMachines.States
{
  public class BootstrapState : IGameState
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IStateMachine<IGameLoopState> _gameLoopStateMachine;
    private readonly IStateMachine<IGameState> _gameStateMachine;
    private readonly IGodFactory _godFactory;
    private readonly IStateFactory _stateFactory;
    private readonly IStaticDataService _staticDataService;

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