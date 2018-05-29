using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float movingTurnSpeed = 360;
    [SerializeField] private float stationaryTurnSpeed = 180;
    [SerializeField] private float jumpPower = 12f;

    private Rigidbody rigidbody;
    private bool isGrounded = false;
    private float turnAmount;
    private float forwardAmount;
    private float capsuleHeight;
    private CapsuleCollider capsule;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public void Move(Vector3 move, bool jumping)
    {
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;

        if (isGrounded)
        {
            HandleGroundMovement(jumping);
        }
        else
        {
            HandleAirbourneMovement();
        }
    }
}
