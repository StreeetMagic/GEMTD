﻿using System;
using InfastuctureCore.Services.StateMachineServices.States;

namespace Infrastructure.GameLoopStateMachines.States
{
    public class LoseState : IState
    {
        public event Action<IState> Entered;
        public event Action<IExitableState> Exited;

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}