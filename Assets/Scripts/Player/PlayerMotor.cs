using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float movingTurnSpeed = 360;
    [SerializeField] private float stationaryTurnSpeed = 180;
    [SerializeField] private float jumpPower = 12f;
    [Range(1f, 4f)][SerializeField] private float gravityMultiplier = 2f;
    [SerializeField] private float movementSpeedMultiplier = 1f;
    [SerializeField] private float groundCheckDistance = 0.1f;

    private Rigidbody rigidbody;
    private float K_half = 0.5f;
    private float runCycleLegOffset = 0.2f;
    private PlayerAnimationController animationController;
    private float origGroundCheckDistane;
    private bool isGrounded = true;
    private float turnAmount;
    private float forwardAmount;
    private Vector3 groundNormal;
    private bool applyRootMotion = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animationController = GetComponent<PlayerAnimationController>();

        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        origGroundCheckDistane = groundCheckDistance;
    }

    public void Move(Vector3 moveDirection, bool jumping)
    {
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        moveDirection = transform.InverseTransformDirection(moveDirection);
        CheckGroundStatus();

        moveDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal);
        turnAmount = Mathf.Atan2(moveDirection.x, moveDirection.z);
        forwardAmount = moveDirection.z;

        if (isGrounded)
        {
            HandleGroundMovement(jumping);
        }
        else
        {
            HandleAirbourneMovement();
        }

        animationController.UpdateAnimator(forwardAmount, turnAmount, isGrounded, rigidbody, movementSpeedMultiplier, runCycleLegOffset, K_half);
    }

    private void HandleGroundMovement(bool jumping)
    {
        if (jumping && animationController.animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, rigidbody.velocity.z);
            isGrounded = false;
     
            groundCheckDistance = 0.1f;
        }
    }

    private void HandleAirbourneMovement()
    {
        Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
        rigidbody.AddForce(extraGravityForce);

        groundCheckDistance = rigidbody.velocity.y < 0 ? origGroundCheckDistane : 0.01f;
    }


    private void CheckGroundStatus()
    {
        RaycastHit hit;

        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance), Color.green);

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, groundCheckDistance))
        {
            groundNormal = hit.normal;
            isGrounded = true;
            applyRootMotion = true;
            animationController.UpdateRootMotion(applyRootMotion);
        }
        else
        {
            isGrounded = false;
            Debug.Log("Set here!");
            groundNormal = Vector3.up;
            applyRootMotion = false;
            animationController.UpdateRootMotion(applyRootMotion);
        }
    }
}
