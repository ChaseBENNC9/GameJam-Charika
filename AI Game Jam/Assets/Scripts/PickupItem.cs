using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public bool inRange;
    public GameObject item;

    // Update is called once per frame
    void Update()
    {
        PickUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item") //if the collided object is the player
        {
            inRange = true; //set inRange to true
            item = other.gameObject; //assign the item as the item for pickup
            Debug.Log("In Range");
        }
        Debug.Log("Entered Trigger");
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Item")
        {
            inRange = false;
        }
    }

    private void PickUp()
    {
        //happens only onece/when key press
        if (inRange && Input.GetKeyDown(KeyCode.Mouse0)) //if in range and clicks mouse button
        {
            Debug.Log("Clicked");
            item.transform.parent = gameObject.transform ; //stick to players position
            item.transform.position = gameObject.transform.position + transform.forward * 2; //move the item to the players forward position
            //item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y + 0.5f, item.transform.position.z); //raise it so it looks like it is off the ground
        }
    }
}
