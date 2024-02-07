using Zenject;

namespace Infrastructure.DIC
{
  internal class GodFactory : IGodFactory
  {
    private readonly IInstantiator _instantiator;

    public GodFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public T Create<T>() =>
      _instantiator.Instantiate<T>();
  }
}