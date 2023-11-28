using System.Collections.Generic;
using UnityEngine;

namespace InfastuctureCore.Services.AssetProviderServices
{
    public class AssetProviderService : IAssetProviderService
    {
        public GameObject Instantiate(string path) =>
            Object.Instantiate((GameObject)Resources.Load(path));

        public GameObject Instantiate(string path, Vector3 at) =>
            Object.Instantiate((GameObject)Resources.Load(path), at, Quaternion.identity);

        public T Instantiate<T>() where T : Object =>
            Object.Instantiate((GameObject)Resources.Load(typeof(T).Name)).GetComponent<T>();

        public T Instantiate<T>(string path) where T : Object =>
            Object.Instantiate((GameObject)Resources.Load(path)).GetComponent<T>();

        public T Instantiate<T>(string path, Vector3 at) where T : Object =>
            Object.Instantiate((GameObject)Resources.Load(path), at, Quaternion.identity).GetComponent<T>();

        public T InstantiateScriptableObject<T>() where T : Object =>
            Resources.Load<T>(typeof(T).Name + "SO");
    }
}