using System;

namespace InfastuctureCore.Services.StateMachineServices.States
{
    public interface IState : IExitableState
    {
        event Action<IState> Entered;
        void Enter();
    }
}