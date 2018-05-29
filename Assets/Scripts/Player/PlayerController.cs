using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private Vector3 move;
    private bool jumping;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
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
        //read the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        motor.StartMovingInDirection(x, z);
	}
}
