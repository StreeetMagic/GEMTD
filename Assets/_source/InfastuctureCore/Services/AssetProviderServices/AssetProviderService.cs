using System.Collections.Generic;
using UnityEngine;

namespace InfastuctureCore.Services.AssetProviderServices
{
    public class AssetProviderService : IAssetProviderService
    {
        private Dictionary<string, GameObject> _assets = new();

        public GameObject Instantiate(string path, Vector3 at)
        {
            if (_assets.TryGetValue(path, out GameObject prefab))
                return Object.Instantiate(prefab, at, Quaternion.identity);
            
            _assets[path] = Resources.Load<GameObject>(path);
            
            return Object.Instantiate(_assets[path], at, Quaternion.identity);
        }

        public GameObject Instantiate(string path)
        {
            if (_assets.TryGetValue(path, out GameObject prefab))
                return Object.Instantiate(prefab);
            
            _assets[path] = Resources.Load<GameObject>(path);
            
            return Object.Instantiate(_assets[path]);
        }

        public T Instantiate<T>(string path) where T : Object
        {
            if (_assets.TryGetValue(path, out GameObject prefab))
                return Object.Instantiate(prefab).GetComponent<T>();
            
            _assets[path] = Resources.Load<GameObject>(path);
            
            return Object.Instantiate(_assets[path]).GetComponent<T>();
        }
    }
}