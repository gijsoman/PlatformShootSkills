using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private Transform cameraTransform;

    private bool jumping;
    private Vector3 cameraForward;
    private Vector3 move;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();

        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls. Now using world-relative controls", gameObject);
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

        if (cameraTransform != null)
        {
            cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            move = vertical * cameraForward + horizontal * cameraTransform.right;
        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        motor.Move(move, jumping);
        jumping = false;
	}
}
