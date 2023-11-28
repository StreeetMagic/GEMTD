namespace InfastuctureCore.Services.StaticDataServices
{
    public interface IStaticDataService
    {
        TStaticData Register<TStaticData>(TStaticData data) where TStaticData : IStaticData;

        TStaticData Get<TStaticData>() where TStaticData : IStaticData;
    }
}