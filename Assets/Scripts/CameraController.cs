using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 1f; //
    [SerializeField] private PlayerInput Input;

    void Update()
    {
        Vector2 lookDelta = Input.PlayerActions.Look.ReadValue<Vector2>();

        // 마우스의 움직임이 감지되었을 때만 회전
        if (lookDelta != Vector2.zero)
        {
            if(transform.rotation.x < 90 && transform.rotation.x > -90)
            {
                float mouseX = lookDelta.x * mouseSensitivity * Time.deltaTime;
                float mouseY = lookDelta.y * mouseSensitivity * Time.deltaTime;

                transform.Rotate(Vector3.up, mouseX, Space.World);
                transform.Rotate(Vector3.left, mouseY, Space.Self);
            }
            
        }
    }
}
