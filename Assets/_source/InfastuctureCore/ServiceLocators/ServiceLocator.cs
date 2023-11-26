using InfastuctureCore.Services;

namespace InfastuctureCore.ServiceLocators
{
    public class ServiceLocator
    {
        private static ServiceLocator s_instance;

        public static ServiceLocator Instance => s_instance ??= new ServiceLocator();

        public TService Register<TService>(TService implementation) where TService : IService
        {
            return Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Get<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}