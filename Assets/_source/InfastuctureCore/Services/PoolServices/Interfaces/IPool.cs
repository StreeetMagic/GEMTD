using System;
using UnityEngine;

namespace InfastuctureCore.Services.PoolServices.Interfaces
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