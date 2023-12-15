using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class WinState : IGameLoopStateMachineState
    {
        public void Enter()
        {
            Debug.Log("Entered Win State");
        }

        public void Exit()
        {
            Debug.Log("Exited Win State");
        }
    }
}