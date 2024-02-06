using Infrastructure.Services.StateMachineServices.States;

namespace Infrastructure.Services.StateMachineServices
{
    public interface IStateMachineService<TData> : IService where TData : class
    {
        TData Data { get; }
        IExitableState ActiveState { get; }

        void Enter<TState>() where TState : class, IState;

        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;

        void Enter<TState, TPayload, TPayload2>(TPayload payload, TPayload2 payload2) where TState : class, IPayloadedState<TPayload, TPayload2>;

        TState Get<TState>() where TState : class, IExitableState;

        TState Register<TState>(TState implementation) where TState : IState;
    }
}