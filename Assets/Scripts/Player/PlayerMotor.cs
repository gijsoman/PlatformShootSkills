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

    public void StartMovingInDirection(Vector3 move, bool jumping)
    {
        
    }
}
