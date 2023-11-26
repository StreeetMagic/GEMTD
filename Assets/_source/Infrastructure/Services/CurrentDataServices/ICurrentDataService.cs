using InfastuctureCore.Services;

namespace Infrastructure.Services.CurrentDataServices
{
    public interface ICurrentDataService : IService
    {
        TData Register<TData>(TData implementation);
        
        TData Get<TData>();
    }
}