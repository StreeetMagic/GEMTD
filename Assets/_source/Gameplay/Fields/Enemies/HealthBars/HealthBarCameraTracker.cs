using UnityEngine;

namespace Gameplay.Fields.Enemies.HealthBars
{
    public class HealthBarCameraTracker : MonoBehaviour
    {
        [SerializeField] private RectTransform _healthBar;

        private Camera _camera;

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
            _healthBar.transform.rotation = Quaternion.LookRotation(_camera.transform.forward, _camera.transform.up);
        }
    }
}