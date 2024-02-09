using Infrastructure.Services.StateMachines.States;
using Infrastructure.Utilities;

namespace Infrastructure.Services.StateMachines
{
  public class StateMachine<TMainState> : IStateMachine<TMainState> where TMainState : class, IState
  {
    #region IStateMachine<TMainState> Members

    public IExitableState ActiveState { get; private set; }

    public void Enter<TState>() where TState : class, TMainState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, TMainState, IPayloadedState<TPayload>
    {
      ChangeState<TState>().Enter(payload);
    }

    public void Enter<TState, TPayload, TPayload2>(TPayload payload, TPayload2 payload2)
      where TState : class, TMainState, IPayloadedState<TPayload, TPayload2>
    {
      var state = ChangeState<TState>();
      state.Enter(payload, payload2);
    }

    public TState Register<TState>(TState implementation) where TState : TMainState
    {
      return Implementation<TState>.Instance = implementation;
    }

    public TState Get<TState>() where TState : class, TMainState =>
      Implementation<TState>.Instance;

    #endregion

    private TState ChangeState<TState>() where TState : class, TMainState, IExitableState
    {
      ActiveState?.Exit();
      var state = Get<TState>();
      ActiveState = state;
      return state;
    }
  }
}