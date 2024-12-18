using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3.0f;
    private Vector3 _lastMousePosition;
    private bool _isRotating;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isRotating = true;
            _lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isRotating = false;
        }

        if (!_isRotating) return;
        
        Rotate();
    }

    private void Rotate()
    {
        var deltaMouse = Input.mousePosition - _lastMousePosition;

        var rotationX = deltaMouse.x * rotationSpeed * Time.deltaTime;
        var rotationY = deltaMouse.y * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, -rotationX, Space.World);
        transform.Rotate(Vector3.right, rotationY, Space.World);

        _lastMousePosition = Input.mousePosition;
    }
}