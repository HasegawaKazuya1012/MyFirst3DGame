using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float mouseSensitivity = 2.0f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private float cameraVerticalAngle;

    private float lastRightClickTime = 0f;
    private float doubleClickThreshold = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if(Time.time - lastRightClickTime < doubleClickThreshold)
            {
                cameraVerticalAngle = 0f;
                if(cameraTransform != null)
                {
                    cameraTransform.localEulerAngles = new Vector3(0,0,0);
                }
            }
            lastRightClickTime = Time.time;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        cameraVerticalAngle -= mouseY;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        if(cameraTransform != null)
        {
            cameraTransform.localEulerAngles = new Vector3(cameraVerticalAngle, 0,0);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;

        if(moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }
        rb.velocity = new Vector3(moveDirection.x * speed , rb.velocity.y,moveDirection.z *  speed);
    }
}