using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    public float Speed = 150f;
    public float RotationSpeed = 3.0f;

    public void StartMovingInDirection(float xAxis, float zAxis)
    {
        xAxis = xAxis * Time.deltaTime * Speed;
        zAxis = zAxis * Time.deltaTime * RotationSpeed;
        transform.Rotate(0, xAxis, 0);
        transform.Translate(0, 0, zAxis);
    }
}
