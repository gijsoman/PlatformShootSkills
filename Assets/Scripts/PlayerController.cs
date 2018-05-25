using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movingDirection = new Vector3(moveHorizontal, 0, moveVertical);

        //Remove the log if working.
        Debug.Log(moveHorizontal + " | " + moveVertical);
        motor.MoveInDirection(movingDirection);
	}
}
