using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movingDirection = new Vector3(moveHorizontal, 0, moveVertical);

        if (movingDirection != Vector3.zero)
        {
            motor.StartMovingInDirection(movingDirection);
        }
        else
        {
            motor.StopMoving();
        }
	}
}
