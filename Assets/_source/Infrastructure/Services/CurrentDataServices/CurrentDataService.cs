using InfastuctureCore.Services;

namespace Infrastructure.Services.CurrentDataServices
{
    public interface ICurrentDataService : IService
    {
        TData Register<TData>(TData implementation);
        
        TData Get<TData>();
    }

    public class CurrentDataService : ICurrentDataService
    {
        public TData Register<TData>(TData implementation)
        {
            return Implementation<TData>.DataInstance = implementation;
        }

        public TData Get<TData>()
        {
            return Implementation<TData>.DataInstance;
        }

        private class Implementation<TData>
        {
            public static TData DataInstance;
        }
    }
}