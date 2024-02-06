using UnityEngine;

namespace Infrastructure.Services.AssetProviderServices
{
    public class AssetProviderService : IAssetProviderService
    {
        public T Instantiate<T>() where T : Object =>
            Object.Instantiate((GameObject)Resources.Load(typeof(T).Name)).GetComponent<T>();

        public T Instantiate<T>(string path) where T : Object =>
            Object.Instantiate((GameObject)Resources.Load(path)).GetComponent<T>();

        public T Instantiate<T>(string path, Vector3 at) where T : Object =>
            Object.Instantiate((GameObject)Resources.Load(path), at, Quaternion.identity).GetComponent<T>();

        public T Get<T>() where T : Object =>
            (T)(Resources.Load(typeof(T).Name));

        public T Get<T>(string path) where T : Object =>
            (T)(Resources.Load(path));
    }
}