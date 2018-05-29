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

    public void UpdateAnimator()
    {

    }
    
}
