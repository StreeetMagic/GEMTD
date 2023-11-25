using InfastuctureCore.Services;

namespace InfastuctureCore.ServiceLocators
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;

        public static ServiceLocator Instance => _instance ??= new ServiceLocator();

        public TService Register<TService>(TService implementation) where TService : IService
        {
            return Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Get<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}