using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    public class PrototypeState : IGameStateMachineState
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