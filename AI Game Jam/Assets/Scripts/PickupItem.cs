using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public bool inRange;
    public GameObject item;
    public bool isPickedUp;

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
        if (inRange && Input.GetKeyDown(KeyCode.Mouse0)) //if in range and clicks mouse button
        {
            Debug.Log("Clicked");
            item.transform.position = new Vector3(item.transform.position.x + 1, item.transform.position.y + 0.5f, item.transform.position.z); //raise it so it looks like it is off the ground
            isPickedUp = !isPickedUp; //set isPickedUp to true
        }
        if (isPickedUp)
        {
            item.transform.parent = gameObject.transform; //stick to players position

        }
    }
}
