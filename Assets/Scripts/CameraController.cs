using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float keyboardCameraMoveSpeed = 10.0f;

    private void Update()
    {
        MoveCameraWithKeyboard();
    }

    private void MoveCameraWithKeyboard()
    {
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            position.y += keyboardCameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= keyboardCameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            position.y -= keyboardCameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position.x += keyboardCameraMoveSpeed * Time.deltaTime;
        }
        transform.position = position;
    }
}
