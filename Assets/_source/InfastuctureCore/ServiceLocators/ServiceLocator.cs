using InfastuctureCore.Services;
using Infrastructure.Services.CurrentDataServices;

namespace InfastuctureCore.ServiceLocators
{
    public class ServiceLocator : IStorageService
    {
        private static IStorageService s_instance;
        public static IStorageService Instance => s_instance ??= new ServiceLocator();
    }
}