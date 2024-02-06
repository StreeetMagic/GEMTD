using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using Infrastructure.Services.StateMachineServices;
using UnityEngine;

namespace Games
{
  public class Game
  {
    private readonly IStateMachineService _gameStateMachine;

    public Game(MonoBehaviour coroutineRunner, string initialSceneName)
    {
      _gameStateMachine = new StateMachineService();
      
      _gameStateMachine.Enter<BootstrapState>();
    }
  }
}