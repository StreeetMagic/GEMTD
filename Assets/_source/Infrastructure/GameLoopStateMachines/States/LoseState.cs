using System;
using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class LoseState : IState
    {
        public event Action<IState> Entered;
        public event Action<IExitableState> Exited;

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