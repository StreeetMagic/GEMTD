using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class LoseState : IGameLoopStateMachineState
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