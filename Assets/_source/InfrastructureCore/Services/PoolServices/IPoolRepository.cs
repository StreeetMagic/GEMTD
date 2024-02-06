using InfastuctureCore.Services.PoolServices.Interfaces;
using UnityEngine;

namespace InfastuctureCore.Services.PoolServices
{
    public interface IPoolRepositoryService : IService
    {
        public T Get<T>() where T : MonoBehaviour, IPoolable<T>;
        public void Release<T>(T obj) where T : MonoBehaviour, IPoolable<T>;
        public void Register<T>(IPool pool) where T : MonoBehaviour, IPoolable<T>;
        public void ReleaseAll<T>() where T : MonoBehaviour, IPoolable<T>;
        public void ReleaseAll();
    }
}