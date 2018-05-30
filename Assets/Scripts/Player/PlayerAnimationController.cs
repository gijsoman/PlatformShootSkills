using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private float animationSpeedMultiplier;

    public Animator animator;
    private PlayerMotor playerMotor;

    private bool grounded = true;
    private float moveSpeedMultiplier;
    private Rigidbody rigidbody;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMotor = GetComponent<PlayerMotor>();
    }

    public void UpdateAnimator(Vector3 _move, float _forwardAmount, float _turnAmount, bool _isGrounded, Rigidbody _rigidbody, float _moveSpeedMultiplier, float _runCycleLegOffset, float _k_Half)
    {
        rigidbody = _rigidbody;
        moveSpeedMultiplier = _moveSpeedMultiplier;
        grounded = _isGrounded;
        animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
        animator.SetBool("OnGround", _isGrounded);
        if (!_isGrounded)
        {
            animator.SetFloat("Jump", _rigidbody.velocity.y);
        }

        float runCycle =
                Mathf.Repeat(
                    animator.GetCurrentAnimatorStateInfo(0).normalizedTime + _runCycleLegOffset, 1);
        float jumpLeg = (runCycle < _k_Half ? 1 : -1) * _forwardAmount;
        if (_isGrounded)
        {
            animator.SetFloat("JumpLeg", jumpLeg);
        }

        if (_isGrounded && _move.magnitude > 0)
        {
            animator.speed = animationSpeedMultiplier;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    public void UpdateRootMotion(bool _applyRootMotion)
    {
        animator.applyRootMotion = _applyRootMotion;
    }

    public void OnAnimatorMove()
    {
        if (grounded && Time.deltaTime > 0)
        {
            Vector3 v = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

            v.y = rigidbody.velocity.y;
            rigidbody.velocity = v;
        }
    }

}
