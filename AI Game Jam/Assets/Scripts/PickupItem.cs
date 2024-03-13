/*
* Description: This script is used to pick up items it also checks for when the player is in range of the item and the goal
* Author: Erika Stuart
* Last Modified: 13 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool inRange , inRangeGoal;
    private GameObject item;
    private Vector3 itemScale;
    private GameObject itemGoal;
    private CharacterController controller; //Character controller component
    private int heldItems;
    private const int MAXITEMS = 1;

    // Update is called once per frame
    void Start()
    {
        heldItems = 0;
        controller = gameObject.GetComponent<CharacterController>(); //Gets the character controller component

    }   
    void Update()
    {
        PickUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item") //if the collided object is an item
        {
            inRange = true; //set inRange to true
            item = other.gameObject; //assign the item as the item for pickup
        }
        else if(other.gameObject.tag == "ItemGoal") //if the collided object is a goal
        {
            inRangeGoal = true; //set inRangeGoal to true
            itemGoal = other.gameObject; //assign the itemGoal as the goal for the item
            ItemGoal currentGoal = itemGoal.GetComponent<ItemGoal>(); //get the itemGoal component
            if(item.gameObject != null && item.gameObject.name == currentGoal.itemName ) //if the item name is the same as the goal name
            {
                currentGoal.ShowHint(true,currentGoal.hintWithItem); //show the hint for the goal wnen the player has the item
            }
            else
            {
                itemGoal.GetComponent<ItemGoal>().ShowHint(true,currentGoal.hintNoItem); //show the hint for the goal when the player does not have the item
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Item")
        {
            inRange = false;
        }
        else if(collider.gameObject.tag == "ItemGoal")
        {
            inRangeGoal = false;
            itemGoal.GetComponent<ItemGoal>().ShowHint(false,"");
        }
    }

    private void PickUp()
    {
        //happens only once/when key press
        if (inRange && Input.GetKeyDown(KeyCode.Mouse0) && heldItems < MAXITEMS ) //if in range and clicks mouse button and not currently holding anything
        {
            Rigidbody rb = item.GetComponent<Rigidbody>(); //get the rigidbody component of the item
            if(rb != null) //if the item has a rigidbody
            {
                Destroy(rb); //destroy the rigidbody
            }
            item.transform.parent = gameObject.transform ; //stick to players position
            item.transform.localRotation = Quaternion.Euler(0,0,0); //reset the rotation of the item

            item.transform.position = gameObject.transform.position + transform.forward * 0.7f; //move the item to the players forward position
            heldItems++; //increment held items
            item.transform.localScale = item.GetComponent<Item>().heldScale; //change the scale of the item
            controller.radius = 1.0f;
        }
        else if (inRangeGoal && Input.GetKeyDown(KeyCode.Mouse0) && heldItems > 0 && item.gameObject.name == itemGoal.GetComponent<ItemGoal>().itemName) //if in range of goal and clicks mouse button and holding an item with the correct name
        {
            item.transform.rotation = Quaternion.Euler(0,0,0); //reset the rotation of the item
            item.transform.localScale = item.GetComponent<Item>().PlacedScale; //change the scale of the item
            item.transform.rotation = itemGoal.transform.rotation; //rotate the item to the rotation of the goal
            item.transform.parent = itemGoal.transform ; //remove the item from the player
            item.transform.position = itemGoal.transform.position ;//move the item to the position of the goal
            controller.radius = 0.5f;


            heldItems--; //decrement held items

            itemGoal.GetComponent<ItemGoal>().UseObject();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && heldItems > 0) //if right mouse button is clicked and holding an item
        {
            Rigidbody rb = item.AddComponent<Rigidbody>(); //add a rigidbody to the item
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; //freeze the x and z position of the item
            item.transform.parent = GameObject.Find("Level").transform; //remove the item from the player
            item.transform.localScale = item.GetComponent<Item>().PlacedScale; //change the scale of the item
            item.transform.position = gameObject.transform.position + transform.forward * 1.5f; //move the item to the players forward position
            heldItems--; //decrement held items
            controller.radius = 0.5f;
        }
    }

}
