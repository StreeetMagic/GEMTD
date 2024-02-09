using Infrastructure.Services.StateMachines.States;
using Zenject;

namespace Infrastructure.Services.StateMachines.StateFactories
{
  public class StateFactory : IStateFactory
  {
    private readonly IInstantiator _instantiator;

    public StateFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    #region IStateFactory Members

    public TState Create<TState>() where TState : IExitableState =>
      _instantiator.Instantiate<TState>();

    #endregion
  }

  public interface IStateFactory : IService
  {
    TState Create<TState>() where TState : IExitableState;
  }
}