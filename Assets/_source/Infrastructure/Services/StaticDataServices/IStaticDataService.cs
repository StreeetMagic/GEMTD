using InfastuctureCore.Services;

namespace Infrastructure.Services.StaticDataServices
{
    public interface IStaticDataService : IService
    {
        TConfig Register<TConfig>(TConfig implementation);
        TConfig Get<TConfig>();
    }
}