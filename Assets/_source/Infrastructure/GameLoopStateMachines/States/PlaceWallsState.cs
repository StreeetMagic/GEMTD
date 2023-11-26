using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines
{
    public class PlaceWallsState : IState
    {
        public void Enter()
        {
            Debug.Log(" Entered PlaceWallsState");
        }

        public void Exit()
        {
            Debug.Log("Exited PlaceWallsState");
        }
    }
}