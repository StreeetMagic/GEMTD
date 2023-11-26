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
}