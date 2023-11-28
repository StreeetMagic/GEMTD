namespace InfastuctureCore.Services.StaticDataServices
{
    public class StaticDataService : IStaticDataService
    {
        public TStaticData Register<TStaticData>(TStaticData implementation) where TStaticData : IStaticData =>
            Implementation<TStaticData>.Instance = implementation;

        public TStaticData Get<TStaticData>() where TStaticData : IStaticData =>
            Implementation<TStaticData>.Instance;
    }
}