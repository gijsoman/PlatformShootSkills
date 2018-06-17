using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    public Animator PlayerAnimator;

    [SerializeField] private float animationSpeedMultiplier;

    private PlayerMotor playerMotor;
    private Rigidbody rb;

    private bool isGrounded;
    private float moveSpeedMultiplier;

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        playerMotor = GetComponent<PlayerMotor>();
    }

    public void UpdateAnimator(Vector3 _move, float _forwardAmount, float _turnAmount, bool _isGrounded, Rigidbody _rigidbody, float _moveSpeedMultiplier, float _runCycleLegOffset, float _half)
    {
        rb = _rigidbody;
        moveSpeedMultiplier = _moveSpeedMultiplier;
        isGrounded = _isGrounded;

        PlayerAnimator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
        PlayerAnimator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
        PlayerAnimator.SetBool("OnGround", _isGrounded);

        float runCycle =Mathf.Repeat(PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + _runCycleLegOffset, 1);
        float jumpLeg = (runCycle < _half ? 1 : -1) * _forwardAmount;
        if (isGrounded)
        {
            PlayerAnimator.SetFloat("JumpLeg", jumpLeg);
        }

        if (!_isGrounded)
        {
            PlayerAnimator.SetFloat("Jump", _rigidbody.velocity.y);
        }
        if (_isGrounded && _move.magnitude > 0)
        {
            PlayerAnimator.speed = animationSpeedMultiplier;
        }
        else
        {
            PlayerAnimator.speed = 1f;
        }
    }

    public void UpdateRootMotion(bool _applyRootMotion)
    {
        PlayerAnimator.applyRootMotion = _applyRootMotion;
    }

    public void OnAnimatorMove()
    {
        if (isGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (PlayerAnimator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

            v.y = rb.velocity.y;
            rb.velocity = v;
        }
    }
}
