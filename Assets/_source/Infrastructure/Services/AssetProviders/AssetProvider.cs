using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastructure.Services.AssetProviders
{
  public class AssetProvider : IAssetProvider
  {
    public T Get<T>() where T : MonoBehaviour
    {
      if (Resources.Load(typeof(T).Name) == null)
      {
        throw new Exception("Asset not found: " + typeof(T).Name);
      }

      var load = Resources.Load(typeof(T).Name).GetComponent<T>();

      if (load == null)
      {
        throw new Exception("Asset not found: " + typeof(T).Name);
      }

      return load;
    }

    public T Get<T>(string path) where T : MonoBehaviour
    {
      var load = Resources.Load(path).GetComponent<T>();

      if (load == null)
      {
        throw new Exception("Asset not found: " + path);
      }

      return load;
    }

    public Material GetMaterial(string path) =>
      Resources.Load<Material>(path);
  }
}