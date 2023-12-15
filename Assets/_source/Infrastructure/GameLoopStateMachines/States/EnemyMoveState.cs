using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StateMachineServices.States;
using Infrastructure.Services.CurrentDataServices;
using UnityEngine;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class EnemyMoveState : IGameLoopStateMachineState
    {
        private IStateMachineService<GameLoopStateMachineData> _gameLoopStateMachine;

        public EnemyMoveState(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            _gameLoopStateMachine = gameLoopStateMachine;
        }

        private ICurrentDataService CurrentDataService => ServiceLocator.Instance.Get<ICurrentDataService>();

        public void Enter()
        {
            Debug.Log(" Entered EnemyMoveState");

            CurrentDataService.FieldModel.EnemySpawnerModel.Spawn(() => { _gameLoopStateMachine.Enter<PlaceWallsState>(); });
        }

        public void Exit()
        {
            Debug.Log("Exited EnemyMoveState");
        }
    }
}