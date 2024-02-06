using UnityEngine;

namespace Infrastructure.Services.StaticDataServices
{
    public interface IStaticDataService : IService
    {
        TStaticData Get<TStaticData>() where TStaticData : IStaticData;
        void Register<TStaticData>() where TStaticData : Object, IStaticData;
        void Register<TStaticData>(TStaticData staticData) where TStaticData : IStaticData;
    }
}