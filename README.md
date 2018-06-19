# PlatformShootSkills (Working Title) - WIP

So I had this idea of making a platformer like game which takes place in a Retro Wave enviroment. It was a very shallow idea so i decided to just start making it and see where it got me from there. I mainly started this project to improve my Unity and C# skills.

Here I want to show a few things that this project contains.

# Controls
'WASD' or the 'Arrows' to move.
'Space' to Jump.
'I' to open your inventory (I just started with the inventory so it can only store items).
'Mouse' to look around.
'Scroll' to zoom in and out.

# 1. The Character movement.
So for the character movement I used Ethan from the standard assets. Ethan also includes 2 scripts: "ThirdPersonUserControl" and "ThirdPersonCharacter". The ThirdPersonCharacter scripts included the physics and controlling the animator. I always like to have seperate scripts for seperate purposes so I decided to take the scripts apart and make 3 seperate scripts: "PlayerAnimationController", "PlayerMotor" and "PlayerController". 



# 2. The Camera.
The camera was created so it could be easily expanded on. I thought of the behaviors I wanted for the camera and made a different script for every behavior. This is what the camera’s base looks like. 
