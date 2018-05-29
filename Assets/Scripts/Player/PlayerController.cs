using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private Vector3 move;
    private bool jumping = false;

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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move = vertical * Vector3.forward + horizontal * Vector3.right;

        motor.StartMovingInDirection(move, jumping);
        //jumping = false;
	}
}
