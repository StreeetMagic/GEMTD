using System;
using UnityEngine;

namespace Infrastructure.Services.Pools.Interfaces
{
  public interface IPool
  {
    void ForceReleaseAll();
  }

  public interface IPool<T> : IPool where T : MonoBehaviour, IPoolable<T>
  {
    T GetObject();
    void Release(T poolable);
    void ApplyToAllObjects(Action<T> action);
  }
}