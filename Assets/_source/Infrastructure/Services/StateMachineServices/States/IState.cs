namespace Infrastructure.Services.StateMachineServices.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}