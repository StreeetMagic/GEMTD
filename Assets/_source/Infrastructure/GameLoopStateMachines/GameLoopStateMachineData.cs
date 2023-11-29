﻿using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.GameLoopStateMachines.States;

namespace Infrastructure.GameLoopStateMachines
{
    public class GameLoopStateMachineData
    {
        public void RegisterStates(IStateMachineService<GameLoopStateMachineData> gameLoopStateMachine)
        {
            gameLoopStateMachine.Register(new PlaceWallsState(gameLoopStateMachine));
            gameLoopStateMachine.Register(new ChooseTowerState());
            gameLoopStateMachine.Register(new EnemyMoveState(gameLoopStateMachine));
            gameLoopStateMachine.Register(new WinState());
            gameLoopStateMachine.Register(new LoseState());
        }
    }
}