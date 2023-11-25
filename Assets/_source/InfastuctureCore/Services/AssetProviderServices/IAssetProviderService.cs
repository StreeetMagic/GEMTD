using UnityEngine;

namespace InfastuctureCore.Services.AssetProviderServices
{
    public interface IAssetProviderService : IService
    {
        //TODO вероятно стоит сделать методы от T
        
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        T Instantiate<T>(string path) where T : Object;
    }
}