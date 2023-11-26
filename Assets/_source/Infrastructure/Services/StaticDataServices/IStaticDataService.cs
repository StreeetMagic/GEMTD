using InfastuctureCore.Services;

namespace Infrastructure.Services.StaticDataServices
{
    public interface IStaticDataService : IService
    {
        TConfig Register<TConfig>(TConfig implementation);
        TConfig Get<TConfig>();
    }

    class StaticDataService : IStaticDataService
    {
        public TConfig Register<TConfig>(TConfig implementation)
        {
            return Implementation<TConfig>.ConfigInstance = implementation;
        }

        public TConfig Get<TConfig>()
        {
            return Implementation<TConfig>.ConfigInstance;
        }

        private class Implementation<TConfig>
        {
            public static TConfig ConfigInstance;
        }
    }
}