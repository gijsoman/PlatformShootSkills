# PlatformShootSkills (Working Title) - WIP

So I had this idea of making a platformer like game which takes place in a Retro Wave enviroment. It was a very shallow idea so i decided to just start making it and see where it got me from there. I mainly started this project to improve my Unity and C# skills.

Here I want to show a few things that this project contains. Instead of reading the scripts in this readme feel free to download the project.

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

```C#
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

So for picking up Items I had to get a little bit creative. First I thought I could just do it with colliders. But what if my character was colliding with 2 Items at the same time? I ended up casting a sphere cast in a certain direction and if it hit multiple items it calculates the distance between the origin of the spherecast and the item. The closest item is the focused item. Here is how i did it:

```C#
using UnityEngine;
using UnityEngine.UI;

public class FocusInteractable : MonoBehaviour
{
    [SerializeField] private float sphereCastRadius = 0.5f;
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform rayCastOrigin;
    [SerializeField] private Text currentlyFocussingInteractable;

    private Interactable closestObject;

    private float currentHitDistance;
    private float shortestDistance = Mathf.Infinity;

    private void Update ()
    {
        currentlyFocussingInteractable.text = "Focussing Object: ";
        if (closestObject != null)
        {
            closestObject.Focused = false;
            closestObject = null;
        }

        currentHitDistance = maxRayDistance;

        RaycastHit[] hits = Physics.SphereCastAll(rayCastOrigin.position, sphereCastRadius, transform.forward, maxRayDistance, layerMask);  
        foreach (RaycastHit hit in hits)
        {
            currentHitDistance = hit.distance;
            Debug.DrawLine(rayCastOrigin.position, hit.transform.position, Color.red);

            if (currentHitDistance < shortestDistance)
            {
                shortestDistance = currentHitDistance;

                if (closestObject != null)
                {
                    closestObject.Focused = false;
                }

                if (closestObject = hit.transform.gameObject.GetComponent<Interactable>())
                {
                    closestObject.Focused = true;
                    currentlyFocussingInteractable.text = "Focussing Object: " + closestObject.name;
                }
            }
        }

        shortestDistance = Mathf.Infinity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(rayCastOrigin.position, rayCastOrigin.position + transform.forward * currentHitDistance);
        Gizmos.DrawWireSphere(rayCastOrigin.position + transform.forward * currentHitDistance, sphereCastRadius);
    }
}
```

## 4. Inventory System
So I wanted to have an inventory system like Diablo 2 and Path of Exile. This was tough for me to start with. Currently you can only store items in the inventory and not get them out or move them around yet.

First I had to make a grid of slots. I used a nested List for this so the size of the inventory could change while the game was being played. This felt a little bit tricky at the start because I had to create a list of lists. I can imagine that in some cases you even want a list of lists containing lists. Here is how I did it:

```C#
 private void CreateSlots()
    {
        for (int y = 0; y < AmountOfSlots.y; y++)
        {
            List<GameObject> slotsSubList = new List<GameObject>();
            for (int x = 0; x < AmountOfSlots.x; x++)
            {
                GameObject slot = new GameObject("Slot " + x + ", " + y);
                
                slot.transform.SetParent(transform);
                Slot currentSlot = slot.AddComponent<Slot>();
                RectTransform slotRect = slot.GetComponent<RectTransform>();
                currentSlot.positionInGrid = new Vector2Int(x, y);
                currentSlot.transform.localScale = Vector3.one;
                slotRect.sizeDelta = new Vector2(SlotWidthAndHeight, SlotWidthAndHeight);
                slotRect.pivot = new Vector2(0,1);
                slotRect.anchorMin = new Vector2(0,1);
                slotRect.anchorMax = new Vector2(0,1);
                slotRect.anchoredPosition = new Vector2(gridStartPosition + x * SlotWidthAndHeight + slotPadding * x, gridStartPosition - y * SlotWidthAndHeight - slotPadding * y);
                slotsSubList.Add(slot);
            }
            Slots.Add(slotsSubList);
        }
    }
```
After making the grid I needed something to manage it. Currently there can only be Items stored in the Inventory. Because this script is already pretty long I decided that in the future I will make seperate scripts for seperate gridmanagement related tasks. That's why the current script is calld "SlotGridStorageManager". 

## Future adittions / TODO
* Finishing the inventory system.
* Adding a shooting mechanic.
* Adding a skillbar with different skills.
* Adding Enemies.
* Optimizing player movement. 
* Adding upgrades.
* Adding Skill trees
* Adding some climbing mechanic.
* Anything Else I can think of that sounds fun or useful to make.


