using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class ChooseTowerState : IGameLoopStateMachineState 
    {
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