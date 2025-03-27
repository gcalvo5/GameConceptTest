using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform target;

    [SerializeField] private float zoomSpeed = 10.0f;
    [SerializeField] private float minZoom = 5.0f;
    [SerializeField] private float maxZoom = 20.0f;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float edgeThreshold = 10.0f;

    private Camera _camera;
    CustomAction input;
    private bool lockedCamera = true;

    private void Awake()
    {
        input = new CustomAction();
        input.Enable();
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        input.Main.zoom.performed += x => OnZoom(x.ReadValue<float>());
        input.Main.FixCamera.performed += ctx => OnLock();
    }

    private void OnDisable()
    {
        input.Main.zoom.performed -= x => OnZoom(x.ReadValue<float>());
    }
    void Update()
    {
        if (lockedCamera)
        {
            transform.position = new Vector3(target.position.x - 6.24f, transform.position.y, target.position.z - 5.99f);
        }
        else
        {
            HandleCameraMovement();
        }
    }
    private void HandleCameraMovement()
    {
        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition.x <= edgeThreshold)
        {
            // Mover la cámara hacia la izquierda
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (mousePosition.x >= Screen.width - edgeThreshold)
        {
            // Mover la cámara hacia la derecha
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        if (mousePosition.y <= edgeThreshold)
        {
            // Mover la cámara hacia abajo
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }
        else if (mousePosition.y >= Screen.height - edgeThreshold)
        {
            // Mover la cámara hacia arriba
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
    }
    private void OnZoom(float y)
    {
        float newZoom = Mathf.Clamp(_camera.orthographicSize - y * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
        _camera.orthographicSize = newZoom;
    }
    private void OnLock()
    {
        lockedCamera = !lockedCamera;
    }
}
