using System;
using Infrastructure.Services.AssetProviders;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.ZenjectFactory
{
  public class ZenjectFactory : IZenjectFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly DiContainer _container;

    public ZenjectFactory(DiContainer container, IAssetProvider assetProvider)
    {
      if (container == null)
      {
        throw new ArgumentNullException(nameof(container));
      }

      _container = container;
      _assetProvider = assetProvider;
    }

    #region IZenjectFactory Members

    public GameObject Instantiate(GameObject gameObject) =>
      _container
        .InstantiatePrefab(gameObject);

    public GameObject Instantiate(GameObject gameObject, Transform parent) =>
      _container
        .InstantiatePrefab(gameObject, parent);

    public TMono Instantiate<TMono>() where TMono : MonoBehaviour =>
      _container
        .InstantiatePrefab(_assetProvider.Get<TMono>())
        .GetComponent<TMono>();

    public TMono Instantiate<TMono>(Transform parent) where TMono : MonoBehaviour
    {
      var monoBehaviour = _container
        .InstantiatePrefab(_assetProvider.Get<TMono>(), parent)
        .GetComponent<TMono>();

      monoBehaviour.transform.parent = parent;

      return monoBehaviour;
    }

    public TMono Instantiate<TMono>(TMono behaviour) where TMono : MonoBehaviour =>
      _container
        .InstantiatePrefab(behaviour)
        .GetComponent<TMono>();

    public TMono Instantiate<TMono>(Vector3 position) where TMono : MonoBehaviour
    {
      var monoBehaviour = _container
        .InstantiatePrefab(_assetProvider.Get<TMono>())
        .GetComponent<TMono>();

      monoBehaviour.transform.position = position;

      return monoBehaviour;
    }

    public TMono Instantiate<TMono>(TMono behaviour, Transform parent) where TMono : MonoBehaviour =>
      _container
        .InstantiatePrefab(behaviour, parent)
        .GetComponent<TMono>();

    public TMono Instantiate<TMono>(TMono behaviour, Vector3 position, Transform parent = null) where TMono : MonoBehaviour =>
      _container
        .InstantiatePrefab(behaviour, position, Quaternion.identity, parent).GetComponent<TMono>();

    public TMono Instantiate<TMono>(TMono behaviour, Vector3 position, Quaternion quaternion, Transform parent = null) where TMono : MonoBehaviour =>
      _container
        .InstantiatePrefab(behaviour, position, quaternion, parent)
        .GetComponent<TMono>();

    #endregion
  }
}