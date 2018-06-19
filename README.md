# PlatformShootSkills (Working Title) - WIP

So I had this idea of making a platformer like game which takes place in a Retro Wave enviroment. It was a very shallow idea so i decided to just start making it and see where it got me from there. I mainly started this project to improve my Unity and C# skills.

Here I want to show a few things that this project contains.

## Controls
* 'WASD' or the 'Arrows' to move.
* 'Space' to Jump.
* 'I' to open your inventory (I just started with the inventory so it can only store items).
* 'Mouse' to look around.
* 'Scroll' to zoom in and out.

## 1. The Character movement.
So for the character movement I used Ethan from the standard assets. Ethan also includes 2 scripts: "ThirdPersonUserControl" and "ThirdPersonCharacter". The ThirdPersonCharacter scripts included the physics and controlling the animator. I always like to have seperate scripts for seperate purposes so I decided to take the scripts apart and make 3 seperate scripts: "PlayerAnimationController", "PlayerMotor" and "PlayerController". 

![Player Scripts](/Images/Character.PNG)

## 2. The Camera.
The camera was created so it could be easily expanded on. I thought of the behaviors I wanted for the camera and made a different script for every behavior. This is what the camera’s base looks like: 

```Unity
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool SameRotationAsCharacter;
    public float Pitch { get; set; }
    public float Yaw { get; set; }
    public float DistanceFromTarget = 1f;
    public float RotationSmoothTime = 0.05f;

    [SerializeField] private Transform target;
    [SerializeField] private float pitchMin = -40;
    [SerializeField] private float pitchMax = 80;

    private Vector3 rotationVelocity;
    private Vector3 currentRotation;

    private void LateUpdate()
    {
        if (SameRotationAsCharacter)
        {
            currentRotation = target.eulerAngles;
        }
        else
        {
            Pitch = Mathf.Clamp(Pitch, pitchMin, pitchMax);
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(Pitch, Yaw), ref rotationVelocity, RotationSmoothTime);
        }

        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * DistanceFromTarget;
    }
}
```

So the variables of this base can be easily modified by other scripts. Also this base doesn’t have to know anything of these other scripts because they only add behavior. These are all the camera scripts I have so far:

![Camera Scripts](/Images/Camera.PNG)

## 3. Interacting with objects.
So for the interaction with objects I made a base class that handles the basic interaction and keeps track of the focus of the object. Because different objects should do different things when interacted with you can easily Inherit this base class to write your future interaction. Currently I only have a script to pick up the items in the world. 

## 4. Inventory System
So I wanted to have an inventory system like Diablo 2 and Path of Exile. This was tough for me to start with. Currently you can only store items in the inventory and not get them out or move them around yet.

First I had to make a grid of slots. I used a nested List for this so the size of the inventory could change while the game was being played. This felt a little bit tricky at the start because I had to create a list of lists. I can imagine that in some cases you even want a list of lists containing lists. 



