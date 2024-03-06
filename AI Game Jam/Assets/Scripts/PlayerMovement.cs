/*
* Description: Manages the Player Movement, uses the built in character controller component in unity
* Author: Chase Bennett-Hill
* Last Modified: 7 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller; //Character controller component
    private Vector3 playerVelocity; //Player velocity value based on x y and z axis
    [SerializeField] private bool groundedPlayer; //Checks if the player is touching the ground
    [SerializeField] private float playerSpeed = 4.0f; //Player speed
    [SerializeField] private float jumpHeight = 1.0f; //Jump height
    private float gravityValue = -9.81f; //Gravity value
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>(); //Gets the character controller component
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) //If the player is touching the ground and the player velocity is less than 0
        {
            playerVelocity.y = 0f; //Ensures that the player doesnt move when it is touching the ground
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //Sets move to the input of each axis (WASD or arrow keys)
        controller.Move(move * Time.deltaTime * playerSpeed); //moves the player based on the input of the player
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move; //Changes the direction the player is facing based on the input of the player
        }
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer) //If the player presses the jump button and is touching the ground
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue); //Jumping logic
        }

        playerVelocity.y += gravityValue * Time.deltaTime; //Gravity logic
        controller.Move(playerVelocity * Time.deltaTime); //Moves the player based on the gravity and jump logic
    }

    

    //  private void OnControllerColliderHit(ControllerColliderHit hit) //IGNORE FOR NOW: Pushing Physics logic but may not be used in the future left in just in case
    // {

    //     if (hit.transform.CompareTag("Movable_Object")) 
    //     {
    //         Rigidbody body = hit.collider.attachedRigidbody;
    //         if (body == null || body.isKinematic) return;
    //         Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
    //         body.velocity = pushDir * 1;
    //         print("xjjd");
    //     }

    // }
}

