using Games;
using Infrastructure.SceneLoaders;
using Infrastructure.Services.CoroutineRunners;
using Infrastructure.Services.StateMachines.GameLoopStateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StateMachines.States;
using Zenject;

namespace Infrastructure.Services.StateMachines.GameStateMachines.States
{
  public class LoadLevelState : IGameState, IPayloadedState<string>
  {
    private readonly IStateMachine<IGameState> _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadLevelState(IStateMachine<IGameState>  gameStateMachine, [Inject(Id = Constants.Ids.InitialSceneName)] string initialSceneName,
      ICoroutineRunner coroutineRunner)
    {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = new SceneLoader(initialSceneName, coroutineRunner);
    }

    public void Enter(string sceneName)
    {
      _sceneLoader.Load(sceneName, OnSceneLoaded);
    }

    public void Enter()
    {
      _sceneLoader.Load(OnSceneLoaded);
    }

    public void Exit()
    {
    }

    private void OnSceneLoaded(string name)
    {
      if (name == Constants.Scenes.Prototype)
        _gameStateMachine.Enter<PrototypeState>();
      else
        _gameStateMachine.Enter<GameLoopState>();
    }
  }
}