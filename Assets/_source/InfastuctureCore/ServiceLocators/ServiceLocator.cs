using InfastuctureCore.Utilities;

namespace InfastuctureCore.ServiceLocators
{
    public class ServiceLocator
    {
        private static ServiceLocator s_instance;

        public static ServiceLocator Instance => s_instance ??= new ServiceLocator();

        public TService Register<TService>(TService implementation) =>
            Implementation<TService>.Instance = implementation;

        public TService Get<TService>() =>
            Implementation<TService>.Instance;
    }
}