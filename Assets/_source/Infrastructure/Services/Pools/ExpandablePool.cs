using System;
using System.Collections.Generic;
using Infrastructure.Services.Pools.Interfaces;
using Infrastructure.Services.ZenjectFactory;
using UnityEngine;

namespace Infrastructure.Services.Pools
{
  public class ExpandablePool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
  {
    private readonly string _assetPath;
    private readonly Transform _parent;
    private readonly Queue<T> _pooledObjects = new();
    private readonly List<T> _wanderingObjects = new();
    private readonly IZenjectFactory _zenjectFactory;

    public ExpandablePool(IZenjectFactory zenjectFactory, Transform parent, string assetPath, int size)
    {
      _zenjectFactory = zenjectFactory;
      _parent = parent;
      _assetPath = assetPath;
      Initialize(size);
    }

    public void Release(T poolable)
    {
      poolable.OnRelease();
      poolable.transform.SetParent(_parent);
      poolable.GameObject.SetActive(false);

      _wanderingObjects.Remove(poolable);
      _pooledObjects.Enqueue(poolable);
    }

    public T GetObject()
    {
      if (_pooledObjects.Count == 0)
        ExpandPool();

      T poolable = _pooledObjects.Dequeue();
      _wanderingObjects.Add(poolable);

      poolable.GameObject.SetActive(true);

      return poolable;
    }

    public void ApplyToAllObjects(Action<T> action)
    {
      foreach (T poolable in _pooledObjects)
        action.Invoke(poolable);

      foreach (T poolable in _wanderingObjects)
        action.Invoke(poolable);
    }

    public void ForceReleaseAll()
    {
      for (int i = _wanderingObjects.Count - 1; i >= 0; i--)
      {
        Release(_wanderingObjects[i]);
      }
    }

    private void ExpandPool()
    {
      var poolable = _zenjectFactory.Instantiate<T>();

      if (poolable == null)
      {
        Debug.LogError("Factory Returned Null");
        return;
      }

      poolable.GameObject.SetActive(false);
      poolable.SetPool(this);
      poolable.transform.SetParent(_parent);
      _pooledObjects.Enqueue(poolable);
    }

    private void Initialize(int size)
    {
      for (var i = 0; i < size; i++)
        ExpandPool();
    }
  }
}