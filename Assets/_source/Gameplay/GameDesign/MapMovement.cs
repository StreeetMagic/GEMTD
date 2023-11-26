using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool _isDragging = false;
    private Vector3 _dragOrigin;
    private float _dragSpeed = 2.0f;
    public float movementSpeed = 250f;
    public float zoomSpeed = 10f;

    void Update()
    {
        MoveWithMouseWheel();
        MoveWithKeys();
        ChangeHeightWithMouseWheel();
    }

    private void ChangeHeightWithMouseWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(Vector3.down * scroll * zoomSpeed, Space.World);
    }

    private void MoveWithKeys()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical) * (movementSpeed * Time.deltaTime);
        transform.Translate(movement, Space.World);
    }

    private void MoveWithMouseWheel()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _isDragging = true;
            _dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            Vector3 currentPosition = Input.mousePosition;
            Vector3 difference = (currentPosition - _dragOrigin) * (_dragSpeed * Time.deltaTime);

            difference = new Vector3(-difference.x, 0, -difference.y);

            transform.Translate(difference, Space.World);

            _dragOrigin = currentPosition;
        }
    }
}