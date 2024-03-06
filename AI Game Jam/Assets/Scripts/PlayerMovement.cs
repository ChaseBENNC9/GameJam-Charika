using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 4.0f;
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f; //Ensures that the player doesnt move when it is touching the ground
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //Sets move to the input of each axis (WASD or arrow keys)
        controller.Move(move * Time.deltaTime * playerSpeed); //moves the player based on the input of the player, move is multiplied by -1 to make the player move in the correct direction
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }
}

