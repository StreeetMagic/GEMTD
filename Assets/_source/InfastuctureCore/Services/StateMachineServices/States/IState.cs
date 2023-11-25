namespace InfastuctureCore.Services.StateMachineServices
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}