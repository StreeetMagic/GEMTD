using UnityEngine;

namespace InfastuctureCore.Services.AssetProviderServices
{
    public interface IAssetProviderService : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);

        T Instantiate<T>() where T : Object;
        T Instantiate<T>(string path) where T : Object;
        T Instantiate<T>(string path, Vector3 at) where T : Object;
        T Get<T>() where T : Object;
    }
}