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
        }
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
        //happens only once/when key press
        if (inRange && Input.GetKeyDown(KeyCode.Mouse0)) //if in range and clicks mouse button
        {
            item.transform.parent = gameObject.transform ; //stick to players position
            item.transform.position = gameObject.transform.position + transform.forward * 2; //move the item to the players forward position
        }
    }
}
