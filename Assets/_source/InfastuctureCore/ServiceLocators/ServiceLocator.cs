using InfastuctureCore.Services;
using InfastuctureCore.Utilities;

namespace InfastuctureCore.ServiceLocators
{
    public class ServiceLocator
    {
        private static ServiceLocator s_instance;

        public static ServiceLocator Instance => s_instance ??= new ServiceLocator();

        public TService Register<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.Instance = implementation;

        public TService Get<TService>() where TService : IService =>
            Implementation<TService>.Instance;
    }
}