namespace Infrastructure.Services.StateMachines.States
{
  public interface IState : IExitableState
  {
    void Enter();
  }
}