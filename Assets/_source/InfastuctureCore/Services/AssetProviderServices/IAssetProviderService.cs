using UnityEngine;

namespace InfastuctureCore.Services.AssetProviderServices
{
    public interface IAssetProviderService : IService
    {
        T Instantiate<T>() where T : Object;
        T Instantiate<T>(string path) where T : Object;
        T Instantiate<T>(string path, Vector3 at) where T : Object;
        T Get<T>() where T : Object;
        T Get<T>(string path) where T : Object;
    }
}