using Infrastructure.Services.CoroutineRunners;
using Infrastructure.Services.StateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachines.GameStateMachines;
using Infrastructure.Services.StateMachines.GameStateMachines.States;
using Infrastructure.Services.StateMachines.StateFactories;
using Zenject;

namespace Games
{
  public class Game
  {
    private readonly IStateMachine<IGameState> _gameStateMachine;
    private readonly IStateMachine<IGameLoopState> _gameLoppGameLoopStateMachine;
    private readonly string _initialSceneName;
    private readonly IStateFactory _stateFactory;
    private readonly ICoroutineRunner _coroutineRunner;

    public Game(
      IStateMachine<IGameState> gameStateMachine,
      IStateMachine<IGameLoopState> gameLoppGameLoopStateMachine,
      [Inject(Id = Constants.Ids.InitialSceneName)] string initialSceneName,
      IStateFactory stateFactory, 
      ICoroutineRunner coroutineRunner)
    {
      _gameStateMachine = gameStateMachine;
      _gameLoppGameLoopStateMachine = gameLoppGameLoopStateMachine;
      _initialSceneName = initialSceneName;
      _stateFactory = stateFactory;
      _coroutineRunner = coroutineRunner;
    }

    public void Start()
    {
      RegisterGameStateMachineStates();

      _gameStateMachine.Enter<BootstrapState>();
    }

    private void RegisterGameStateMachineStates()
    {
      _gameStateMachine.Register(_stateFactory.Create<BootstrapState>());
      _gameStateMachine.Register(_stateFactory.Create<LoadLevelState>());
      _gameStateMachine.Register(_stateFactory.Create<GameLoopState>());
      _gameStateMachine.Register(_stateFactory.Create<PrototypeState>());
    }
  }
}