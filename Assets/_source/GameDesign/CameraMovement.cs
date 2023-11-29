using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.InputServices;
using Unity.VisualScripting;
using UnityEngine;

namespace GameDesign
{
    public class CameraMovement : MonoBehaviour
    {
        public float movementSpeed = 500f;

        private IInputService InputService => ServiceLocator.Instance.Get<IInputService>();

        void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 cameraMovement = InputService.CameraMovement();

            if (cameraMovement == Vector2.zero)
                return;

            float cameraMovementX = cameraMovement.x * movementSpeed * Time.deltaTime;
            float cameraMovementY = cameraMovement.y * movementSpeed * Time.deltaTime;
            Vector3 position = transform.position;
            Vector3 newPosition = position + new Vector3(cameraMovementX, 0, cameraMovementY);

            float maxDistanceDelta = movementSpeed * Time.deltaTime;
            position = Vector3.MoveTowards(position, newPosition, maxDistanceDelta);
            transform.position = position;
        }
    }
}