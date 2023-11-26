using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services;

namespace Infrastructure.Services.CurrentDataServices
{
    public interface IStorageService : IService
    {
        public TData Register<TData>(TData implementation)
        {
            return Implementation<TData>.TInstance = implementation;
        }

        public TData Get<TData>()
        {
            return Implementation<TData>.TInstance;
        }
    }

    public interface ICurrentDataService : IStorageService
    {
    }

    public class CurrentDataService : ICurrentDataService
    {
    }

    public interface IStaticDataService : IStorageService
    {
    }

    class StaticDataService : IStaticDataService
    {
    }
}