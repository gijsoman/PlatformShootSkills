using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMotor playerMotor;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        animator.SetFloat("Forward", playerMotor.rawZAxis);
        animator.SetFloat("Turn", playerMotor.rawXAxis);
    }
}
