using Infrastructure.Services.StateMachines.States;
using Zenject;

namespace Infrastructure.Services.StateMachines.StateFactories
{
  public class StateFactory : IStateFactory
  {
    private IInstantiator _instantiator;

    public StateFactory(IInstantiator instantiator) =>
      _instantiator = instantiator;

    public TState Create<TState>() where TState : IExitableState =>
      _instantiator.Instantiate<TState>();
  }

  public interface IStateFactory : IService
  {
    TState Create<TState>() where TState : IExitableState;
  }
}