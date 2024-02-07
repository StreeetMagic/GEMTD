using Infrastructure.Services.StateMachines.States;

namespace Infrastructure.Services.StateMachines
{
  public interface IStateMachine<TMainState> : IService where TMainState : class, IState
  {
    IExitableState ActiveState { get; }

    void Enter<TState>() where TState : class, TMainState;
    void Enter<TState, TPayload>(TPayload payload) where TState : class, TMainState, IPayloadedState<TPayload>;
    void Enter<TState, TPayload, TPayload2>(TPayload payload, TPayload2 payload2) where TState : class, TMainState, IPayloadedState<TPayload, TPayload2>;

    TState Register<TState>(TState implementation) where TState : TMainState;
    TState Get<TState>() where TState : class, TMainState;
  }
}