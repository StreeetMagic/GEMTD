using UnityEngine;

namespace Infrastructure.Services.StateMachines.GameStateMachines.States
{
  public class PrototypeState : IGameState
  {
    public void Enter()
    {
      Debug.Log("Entered Prototype State");
    }

    public void Exit()
    {
      Debug.Log("Exited Prototype State");
    }
  }
}