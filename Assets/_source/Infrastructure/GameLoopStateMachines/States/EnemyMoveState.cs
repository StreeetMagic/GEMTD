﻿using Cysharp.Threading.Tasks;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.Services.CurrentDataServices;

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
            CurrentDataService.FieldModel.EnemySpawnerModel.Spawn();
        }

        public void Exit()
        {
        }
    }
}