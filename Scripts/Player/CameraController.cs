using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dampTime = 0.15f;
    public CameraMode cameraMode = CameraMode.NORMAL;
    public Transform target;
    public Vector3 offset = Vector3.zero;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    private Vector3 delta;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (target)
        {
            if (cameraMode == CameraMode.NORMAL)
            {
                Vector3 point = cam.WorldToViewportPoint(target.position);
                delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + offset + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
            else if (cameraMode == CameraMode.TARGETTING)
            {
                float maxScreenPoint = 0.8f;
                Vector3 mousePos = Input.mousePosition * maxScreenPoint + new Vector3(Screen.width, Screen.height, 0f) * ((1f - maxScreenPoint) * 0.5f);
                Vector3 position = (target.position + GetComponent<Camera>().ScreenToWorldPoint(mousePos)) / 2f;
                Vector3 destination = new Vector3(position.x, position.y, -10);
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }

    }

    public void SetOffset(Vector3 _newOffset)
    {
        offset = _newOffset;
    }
}

public enum CameraMode
{
    NORMAL,
    TARGETTING
}
