using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class LoseState : IState
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