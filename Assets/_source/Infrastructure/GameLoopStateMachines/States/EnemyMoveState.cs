using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class EnemyMoveState : IState
    {
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        public EnemyMoveState(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
        }

        public void Enter()
        {
            Debug.Log(" Entered EnemyMoveState");
            _gameLoopStateMachine.Enter<PlaceWallsState>();
        }

        public void Exit()
        {
            Debug.Log("Exited EnemyMoveState");
        }
    }
}