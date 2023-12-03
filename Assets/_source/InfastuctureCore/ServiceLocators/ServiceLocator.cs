using InfastuctureCore.Services;
using Infrastructure.Services.CurrentDataServices;

namespace InfastuctureCore.ServiceLocators
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance => s_instance ??= new ServiceLocator();
        private static ServiceLocator s_instance;

        public TService Register<TService>(TService implementation) => 
            Implementation<TService>.Instance = implementation;

        public TService Get<TService>() => 
            Implementation<TService>.Instance;
        
        public static TService Gets<TService>() => 
            Implementation<TService>.Instance;
    }
}