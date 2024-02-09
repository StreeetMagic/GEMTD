using UnityEngine;

namespace Gameplay.Fields.Enemies.HealthBars
{
  public class HealthBarCameraTracker : MonoBehaviour
  {
    private Camera _camera;
    [SerializeField] private RectTransform _healthBar;

    private void Awake()
    {
      _camera = Camera.main;
    }

    private void LateUpdate()
    {
      RotateToCamera();
    }

    private void RotateToCamera()
    {
      Transform transform1 = _camera.transform;
      _healthBar.transform.rotation = Quaternion.LookRotation(transform1.forward, transform1.up);
    }
  }
}