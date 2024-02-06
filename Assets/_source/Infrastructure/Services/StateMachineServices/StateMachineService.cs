using Infrastructure.Services.StateMachineServices.States;
using Infrastructure.Utilities;

namespace Infrastructure.Services.StateMachineServices
{
    public class StateMachineService : IStateMachineService
    {
        public IExitableState ActiveState { get; private set; }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            ChangeState<TState>().Enter(payload);
        }

        public void Enter<TState, TPayload, TPayload2>(TPayload payload, TPayload2 payload2)
            where TState : class, IPayloadedState<TPayload, TPayload2>
        {
            var state = ChangeState<TState>();
            state.Enter(payload, payload2);
        }

        public TState Get<TState>() where TState : class, IExitableState
        {
            return Implementation<TState>.Instance;
        }

        public TState Register<TState>(TState implementation) where TState : IState
        {
            return Implementation<TState>.Instance = implementation;
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            ActiveState?.Exit();
            var state = Get<TState>();
            ActiveState = state;
            return state;
        }
    }
}