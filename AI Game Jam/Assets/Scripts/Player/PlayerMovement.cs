/*
* Description: Manages the Player Movement, uses the built in character controller component in unity
* Author: Chase Bennett-Hill
* Last Modified: 7 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/


using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller; //Character controller component
    private Vector3 playerVelocity; //Player velocity value based on x y and z axis
    [SerializeField] private bool groundedPlayer; //Checks if the player is touching the ground
    [SerializeField] private float playerSpeed = 4.0f; //Player speed
    [SerializeField] private float jumpHeight = 1.0f; //Jump height
    private const float GRAVITYVALUE = -24f;
    public Animator anim;

    private void Start()
    {

        controller = gameObject.GetComponent<CharacterController>(); //Gets the character controller component

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        anim.SetFloat("Speed", 0);
        if (groundedPlayer && playerVelocity.y < 0) //If the player is touching the ground and the player velocity is less than 0
        {
            playerVelocity.y = 0f; //Ensures that the player doesnt move when it is touching the ground
        }
        Vector3 move = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //Sets move to the input of each axis (WASD or arrow keys)
        controller.Move(playerSpeed * Time.deltaTime * move); //moves the player based on the input of the player
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move; //Changes the direction the player is facing based on the input of the player
            anim.SetFloat("Speed", 1);
        }
        // Changes the height position of the player..
        if (Input.GetButton("Jump") && groundedPlayer) //If the player presses the jump button and is touching the ground
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GRAVITYVALUE);
        }

        playerVelocity.y += GRAVITYVALUE * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}

