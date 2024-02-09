using UnityEngine;

namespace Infrastructure.Services.Pools.Interfaces
{
  public interface IPoolable<T> where T : MonoBehaviour, IPoolable<T>
  {
    public GameObject GameObject { get; }

    public void OnRelease();
    public void SetPool(IPool<T> ownerPool);
  }
}