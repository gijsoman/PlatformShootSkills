using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    public float PlayerSpeed = 5;

    private Rigidbody playerRigidBody;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    public void MoveInDirection(Vector3 direction)
    {
        playerRigidBody.AddForce(direction * PlayerSpeed);
    }
}
