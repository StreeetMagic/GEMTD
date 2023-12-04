using System;
using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class ChooseTowerState : IState
    {
        public event Action<IState> Entered;
        public event Action<IExitableState> Exited;

        public void Enter()
        {
            Debug.Log("Entered ChooseTower State");
        }

        public void Exit()
        {
            Debug.Log("Exited ChooseTower State"); 
        }
    }
}