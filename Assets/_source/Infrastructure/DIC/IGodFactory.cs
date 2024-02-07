using Infrastructure.Services;

namespace Infrastructure.DIC
{
  public interface IGodFactory : IService
  {
    T Create<T>();
  }
}