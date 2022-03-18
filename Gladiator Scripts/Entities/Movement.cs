using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Movement : MonoBehaviour
{
    //Creating important variables. Serialize field lets private values be shown in the inspector
    float speed = 5f;
    public Camera playerCamera;
    float sprint = 10f;
    float jumpHeight = 8.0f;
    float sensitivity = 2.0f;
    float gravity = 20.0f;
    float xlimit = 25.0f;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    GameObject em;
    Player pl;

    void Start()
    {
        //Lets me use the character controller in the code
        characterController = GetComponent<CharacterController>();

        //Hides the cursor and locks it so it doesn't move from the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Gets the GameObject "Entity Manager" and the component of that GameObject "Player" (A script)
        em = GameObject.Find("EntityManager");
        pl = em.GetComponent<Player>();
    }

    void Update()
    {
        //Makes the direction we can move. This is to stop us from being able to fly upwards if we ae looking into the sky and holding W
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Hold LShift to sprint
        bool running = Input.GetKey(KeyCode.LeftShift);

        //Creates the speed that we move at. The ? will pick the output depending on if the boolean is true or false. The first value "sprint" would output if it is true and "speed" will output if it is false
        float curSpeedX = (running ? sprint : speed) * Input.GetAxis("Vertical");
        float curSpeedY = (running ? sprint : speed) * Input.GetAxis("Horizontal");

        //Sets the value of "movementDirectionY" to whatever the y value of the player is
        float movementDirectionY = moveDirection.y;
        //Part one of moving. It multiplies the speed of X and Y with the Z and X value
        moveDirection = (-forward * curSpeedX) + (-right * curSpeedY);

        //If spacebar is pressed and the character is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            //Move us in the direction of y by the height we can jump
            moveDirection.y = jumpHeight;
        }
        else
        {
            //Resets the value
            moveDirection.y = movementDirectionY;
        }

        //Checks if we are in the air or on the ground
        if (!characterController.isGrounded)
        {
            //Moves us to the ground if we are in the air
            moveDirection.y -= gravity * Time.deltaTime;

            /*
             * When I discovered this, I prefered it over using tags
             * This is because its much simplier to write and altogether
             * Makes the code look a LOT cleaner
             */
        }

        //Allows us to restrict movement while in menus.
        if(pl.canMove == true)
        {
            //Actually moves us
            characterController.Move(moveDirection * Time.deltaTime);

            //Moves our camera where our mouse goes
            rotationX += -Input.GetAxis("Mouse Y") * sensitivity;
            //Clamps it so our camera cant move past a certain spot in this case 25 and -25
            rotationX = Mathf.Clamp(rotationX, -xlimit, xlimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 180, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        } else
        {
            //Keeps our original position so we can get pushed or fall through the ground
            gameObject.transform.position = new Vector3(transform.position.x, 1.663347f, transform.position.z);
        }
    }
}

