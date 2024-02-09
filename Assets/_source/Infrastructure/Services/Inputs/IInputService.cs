using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Inputs
{
  public interface IInputService : IService
  {
    Vector2 CameraMovementXZ { get; }
    Vector2 CameraMovementY { get; }
    Vector2 MousePosition { get; }

    bool LeftMouseButtonWasPressedThisFrame { get; }
    bool LeftMouseButtonIsPressed { get; }

    bool RightMouseButtonWasPressedThisFrame { get; }
    bool RightMouseButtonIsPressed { get; }
  }

  public class InputService : IInputService
  {
    private readonly Controls _contols;

    public InputService()
    {
      _contols = new Controls();
      _contols.Enable();

      Debug.Log("Конструктор InputService");
    }

    public bool LeftMouseButtonWasPressedThisFrame => Mouse.current.leftButton.wasPressedThisFrame;
    public bool LeftMouseButtonIsPressed => Mouse.current.leftButton.isPressed;
    public bool RightMouseButtonWasPressedThisFrame => Mouse.current.rightButton.wasPressedThisFrame;
    public bool RightMouseButtonIsPressed => Mouse.current.rightButton.isPressed;

    public Vector2 CameraMovementXZ =>
      _contols.Camera.MoveXZ.ReadValue<Vector2>();

    public Vector2 CameraMovementY =>
      _contols.Camera.MoveY.ReadValue<Vector2>();

    public Vector2 MousePosition =>
      Mouse.current.position.ReadValue();
  }
}