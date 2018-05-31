using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private Transform camera;
    private Vector3 cameraForward;
    private Vector3 move;
    private bool jumping = false;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        if (Camera.main != null)
        {
            camera = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls. Now using world-relative controls", gameObject);
        }
    }

    private void Update()
    {
        if (!jumping)
        {
            jumping = Input.GetButtonDown("Jump");
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (camera != null)
        {
            cameraForward = Vector3.Scale(camera.forward, new Vector3(1, 0, 1)).normalized;
            move = vertical * cameraForward + horizontal * camera.right;
        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        motor.Move(move, jumping);
        jumping = false;
	}
}
