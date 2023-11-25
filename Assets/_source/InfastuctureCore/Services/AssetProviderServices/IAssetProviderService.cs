using UnityEngine;

namespace InfastuctureCore.Services.AssetProviderServices
{
    public interface IAssetProviderService : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        
        GameObject Instantiate(string path);
        
        T Instantiate<T>(string path) where T : Object;
    }
}