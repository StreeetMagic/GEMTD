using InfastuctureCore.Services;
using UnityEngine;

namespace Infrastructure.Services.InputServices
{
    public interface IInputService : IService
    {
        Vector2 CameraMovement();
    }

    public class InputService : IInputService
    {
        private Controls _contols;

        public InputService()
        {
            _contols = new Controls();
            _contols.Enable();
        }

        public Vector2 CameraMovement()
        {
            return _contols.Camera.Move.ReadValue<Vector2>();
        }
    }
}