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
    [SerializeField] private float runCycleLegOffset = 0.2f;
    [SerializeField] private float movementSpeedMultiplier = 1f;
    [SerializeField] private float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    private PlayerAnimationController animationController;

    private bool isGrounded;
    private bool applyRootMotion;
    private float origGroundCheckDistane;
    private float turnAmount;
    private float forwardAmount;
    private const float half = 0.5f;
    private Vector3 groundNormal;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animationController = GetComponent<PlayerAnimationController>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        origGroundCheckDistane = groundCheckDistance;
    }

    public void Move(Vector3 _moveDirection, bool _jumping)
    {
        if (_moveDirection.magnitude > 1f)
        {
            _moveDirection.Normalize();
        }

        _moveDirection = transform.InverseTransformDirection(_moveDirection);
        CheckGroundStatus();
        _moveDirection = Vector3.ProjectOnPlane(_moveDirection, groundNormal);
        turnAmount = Mathf.Atan2(_moveDirection.x, _moveDirection.z);
        forwardAmount = _moveDirection.z;

        ApplyExtraTurnRotation();

        if (isGrounded)
        {
            HandleGroundMovement(_jumping);
        }
        else
        {
            HandleAirbourneMovement();
        }

        animationController.UpdateAnimator(_moveDirection, forwardAmount, turnAmount, isGrounded, rb, movementSpeedMultiplier, runCycleLegOffset, half);
    }

    private void HandleGroundMovement(bool _jumping)
    {
        if (_jumping && animationController.PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
            isGrounded = false;
            applyRootMotion = false;
            animationController.UpdateRootMotion(applyRootMotion);
            groundCheckDistance = 0.1f;
        }
    }

    private void HandleAirbourneMovement()
    {
        Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
        rb.AddForce(extraGravityForce);

        groundCheckDistance = rb.velocity.y < 0 ? origGroundCheckDistane : 0.01f;
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
            groundNormal = Vector3.up;
            applyRootMotion = false;
            animationController.UpdateRootMotion(applyRootMotion);
        }
    }

    private void ApplyExtraTurnRotation()
    {
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed *Time.deltaTime, 0);
    }
}
