using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private float animationSpeedMultiplier;

    public Animator PlayerAnimator;
    private PlayerMotor playerMotor;

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        playerMotor = GetComponent<PlayerMotor>();
    }
    
}
