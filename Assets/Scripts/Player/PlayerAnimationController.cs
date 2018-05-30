using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private float animationSpeedMultiplier;

    private Animator animator;
    private PlayerMotor playerMotor;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMotor = GetComponent<PlayerMotor>();
    }

    public void UpdateAnimator(float forwardAmount, float turnAmount, bool isGrounded, Rigidbody rigidbody, bool applyRootMotion)
    {
        animator.applyRootMotion = applyRootMotion;
        animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        animator.SetBool("OnGround", isGrounded);
        if (!isGrounded)
        {
            animator.SetFloat("Jump", rigidbody.velocity.y);
        }

        animator.speed = 1f;

    }
    
}
