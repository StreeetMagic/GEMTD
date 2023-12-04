using System;

namespace InfastuctureCore.Services.StateMachineServices.States
{
    public interface IExitableState
    {
        event Action<IExitableState> Exited;
        void Exit();
    }
}