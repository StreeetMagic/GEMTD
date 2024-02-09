using UnityEngine;

namespace Infrastructure.Services.StateMachines.GameLoopStateMachines.States
{
  public class LoseState : IGameLoopState
  {
    public void Enter()
    {
      Debug.Log("Entered Lose State");
    }

    public void Exit()
    {
      Debug.Log("Exited Lose State");
    }
  }
}