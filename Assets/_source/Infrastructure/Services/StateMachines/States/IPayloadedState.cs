namespace Infrastructure.Services.StateMachines.States
{
  public interface IPayloadedState<TPayload> : IExitableState
  {
    void Enter(TPayload sceneName);
  }

  public interface IPayloadedState<TPayload, TPayload2> : IExitableState
  {
    void Enter(TPayload mainMenuSceneName, TPayload2 nextSceneName);
  }

  public interface IPayloadedState<TPayload, TPayload2, TPayload3> : IExitableState
  {
    void Enter(TPayload payload, TPayload2 payload2, TPayload3 payload3);
  }

  public interface IPayloadedState<TPayload, TPayload2, TPayload3, TPayload4> : IExitableState
  {
    void Enter(TPayload payload, TPayload2 payload2, TPayload3 payload3, TPayload4 payload4);
  }
}