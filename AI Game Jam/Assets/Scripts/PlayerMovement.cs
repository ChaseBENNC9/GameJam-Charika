using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float playerSpeed = 5f;
    private Vector3 playerPos;
    private bool colliding = false;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = playerPrefab.transform.position;
    }

//  Update is called once per frame
    void Update()
    {
        if(!colliding)
        {
           
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("D key was pressed");
                playerPos.x -= playerSpeed;
                playerPrefab.transform.position = playerPos;
            }
            else if(Input.GetKey(KeyCode.A))
            {
                Debug.Log("A key was pressed");
                playerPos.x += playerSpeed;
                playerPrefab.transform.position = playerPos;
            }
            else if(Input.GetKey(KeyCode.W))
            {
                Debug.Log("W key was pressed");
                playerPos.z -= playerSpeed;
                playerPrefab.transform.position = playerPos;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                Debug.Log("S key was pressed");
                playerPos.z += playerSpeed;
                playerPrefab.transform.position = playerPos;
            }
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            colliding = true;
            print("Colliding with wall");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            colliding = false;
        }
    }
}
