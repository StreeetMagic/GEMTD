namespace InfastuctureCore.Services.StateMachineServices.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}