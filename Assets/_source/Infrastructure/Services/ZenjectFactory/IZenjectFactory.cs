using UnityEngine;

namespace Infrastructure.Services.ZenjectFactory
{
  public interface IZenjectFactory : IService
  {
    GameObject Instantiate(GameObject gameObject);
    GameObject Instantiate(GameObject gameObject, Transform parent);

    TMono Instantiate<TMono>() where TMono : MonoBehaviour;
    TMono Instantiate<TMono>(Transform parent) where TMono : MonoBehaviour;
    TMono Instantiate<TMono>(TMono behaviour) where TMono : MonoBehaviour;
    TMono Instantiate<TMono>(Vector3 position) where TMono : MonoBehaviour;
    TMono Instantiate<TMono>(TMono behaviour, Transform parent) where TMono : MonoBehaviour;
    TMono Instantiate<TMono>(TMono behaviour, Vector3 position, Transform parent = null) where TMono : MonoBehaviour;
    TMono Instantiate<TMono>(TMono behaviour, Vector3 position, Quaternion quaternion, Transform parent = null) where TMono : MonoBehaviour;
  }
}