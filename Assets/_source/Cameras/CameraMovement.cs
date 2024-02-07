using Infrastructure;
using Infrastructure.Services.Inputs;
using UnityEngine;
using Zenject;

namespace Cameras
{
  public class CameraMovement : MonoBehaviour
  {
    [SerializeField] private float _movementSpeed = 50f;
    [SerializeField] private float _scrollPower = 1;

    private IInputService _inputService;
    
    [Inject]
    public void Construct(IInputService inputService)
    {
      _inputService = inputService;
    }
    
    void Update()
    {
      MoveXZ();
      MoveY();
    }

    private void MoveY()
    {
      Vector2 cameraMovement = _inputService.CameraMovementY;
      Transform transform1 = transform;
      Vector3 newPosition = transform1.position - new Vector3(0, cameraMovement.y * _scrollPower * Time.deltaTime, 0);
      transform1.position = newPosition;
    }

    private void MoveXZ()
    {
      Vector2 cameraMovement = _inputService.CameraMovementXZ;

      if (cameraMovement == Vector2.zero)
        return;

      float cameraMovementX = cameraMovement.x * _movementSpeed * Time.deltaTime;
      float cameraMovementY = cameraMovement.y * _movementSpeed * Time.deltaTime;
      Vector3 position = transform.position;
      Vector3 newPosition = position + new Vector3(cameraMovementX, 0, cameraMovementY);

      float maxDistanceDelta = _movementSpeed * Time.deltaTime;
      position = Vector3.MoveTowards(position, newPosition, maxDistanceDelta);
      transform.position = position;
    }
  }
}