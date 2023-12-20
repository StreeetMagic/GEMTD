using UnityEngine;

namespace InfastuctureCore.Services.StaticDataServices
{
    public interface IStaticDataService : IService
    {
        TStaticData Get<TStaticData>() where TStaticData : IStaticData;
        void RegisterScriptable<TStaticData>() where TStaticData : Object, IStaticData;
        void RegisterScript<TStaticData>(TStaticData staticData) where TStaticData : IStaticData;
    }
}