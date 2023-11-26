using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services;

namespace Infrastructure.Services.CurrentDataServices
{
    public interface IStorageService : IService
    {
        TData Register<TData>(TData implementation);
        TData Get<TData>();
    }

    public abstract class StorageService : IStorageService
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

    public class CurrentDataService : StorageService, ICurrentDataService
    {
    }

    public interface IStaticDataService : IStorageService
    {
    }

    class StaticDataService : StorageService, IStaticDataService
    {
    }
    
    
}