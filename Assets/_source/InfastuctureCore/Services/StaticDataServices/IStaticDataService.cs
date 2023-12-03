using UnityEngine;

namespace InfastuctureCore.Services.StaticDataServices
{
    public interface IStaticDataService
    {
        TStaticData Get<TStaticData>() where TStaticData : IStaticData;
        void Register<TStaticData>() where TStaticData : Object, IStaticData;
    }
}