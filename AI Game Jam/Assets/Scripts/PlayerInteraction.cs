/*
* Description: This script is used for player interactions and manages picking up and dropping items
* Author: Erika Stuart
* Last Modified: 15 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool inRange,
        inRangeGoal;
    public GameObject item;
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
        if(item !=null && itemGoal !=null)
            UseObject();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item") //if the collided object is an item
        {
            inRange = true; //set inRange to true
            item = other.gameObject; //assign the item as the item for pickup
        }
        else if (other.gameObject.tag == "ItemGoal") //if the collided object is a goal
        {
            inRangeGoal = true; //set inRangeGoal to true
            itemGoal = other.gameObject; //assign the itemGoal as the goal for the item
            ItemGoal currentGoal = itemGoal.GetComponent<ItemGoal>(); //get the itemGoal component
        }
        else if (other.gameObject.tag == "hintCollider")
        {
            if (
                item != null
                && item.GetComponent<Item>().Type
                    == other.gameObject.transform.parent.GetComponent<ItemGoal>().itemName
            ) //if the item name is the same as the goal name
            {
                other
                    .gameObject.transform.parent.GetComponent<ItemGoal>()
                    .ShowHint(
                        true,
                        other.gameObject.transform.parent.GetComponent<ItemGoal>().hintWithItem
                    ); //show the hint for the goal wnen the player has the item
            }
            else
            {
                other
                    .gameObject.transform.parent.GetComponent<ItemGoal>()
                    .GetComponent<ItemGoal>()
                    .ShowHint(
                        true,
                        other.gameObject.transform.parent.GetComponent<ItemGoal>().hintNoItem
                    ); //show the hint for the goal when the player does not have the item
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Item")
        {
            inRange = false;
        }
        else if (collider.gameObject.tag == "ItemGoal")
        {
            inRangeGoal = false;
            //  itemGoal.GetComponent<ItemGoal>().ShowHint(false);
        }
    }

    private void PickUp()
    {
        //happens only once/when key press
        if (inRange && Input.GetKeyDown(KeyCode.Mouse0) && heldItems < MAXITEMS) //if in range and clicks mouse button and not currently holding anything
        {
            Debug.Log("Picking up item");
            if (item.TryGetComponent<Rigidbody>(out var rb)) //if the item has a rigidbody
            {
                Destroy(rb); //destroy the rigidbody
            }
            
            item.transform.parent = gameObject.transform; //stick to players position
            item.transform.localRotation = Quaternion.Euler(0, 0, 0); //reset the rotation of the item

            item.transform.position = gameObject.transform.position + transform.forward * 0.7f; //move the item to the players forward position
            heldItems++; //increment held items
            item.transform.localScale = item.GetComponent<Item>().HeldScale; //change the scale of the item
            controller.radius = 1.0f;
        }
    }

    private void UseObject()
    {
        if (heldItems > 0)
        {
            if (
                inRangeGoal
                && item.GetComponent<Item>().Type == itemGoal.GetComponent<ItemGoal>().itemName 
            ) //if in range of goal and clicks mouse button and holding an item with the correct name
            {
                UseObjectAtGoal();
            }
            else
            {
                DropObject();
            }
        }
        else if (heldItems == 0 && inRangeGoal && itemGoal.GetComponent<ItemGoal>().IsComplex && Input.GetKeyDown(KeyCode.E))
        {
            if (item.TryGetComponent<BoxCollider>(out var rb)) //if the item has a rigidbody
            {
                Destroy(rb); //destroy the rigidbody
            }
            itemGoal.GetComponent<ItemGoal>().UseComplexPuzzle();
            
        }

    }

    private void UseObjectAtGoal()
    {
        if (Input.GetKeyDown(KeyCode.E)) //if in range and clicks mouse button
        {
            item.transform.rotation = Quaternion.Euler(0, 0, 0); //reset the rotation of the item
            item.transform.rotation = itemGoal.transform.rotation; //rotate the item to the rotation of the goal
            item.transform.localScale = item.GetComponent<Item>().PlacedScale; //change the scale of the item
            item.transform.parent = itemGoal.transform; //remove the item from the player
            item.transform.position = itemGoal.transform.position; //move the item to the position of the goal
            itemGoal.GetComponent<ItemGoal>().UseObject(item);
            controller.radius = 0.5f;
            heldItems--; //decrement held items
        }
    }

    private void DropObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) //if in range and clicks mouse button
        {
            Rigidbody rb = item.AddComponent<Rigidbody>(); //add a rigidbody to the item
            rb.constraints =
                RigidbodyConstraints.FreezePositionX
                | RigidbodyConstraints.FreezePositionZ
                | RigidbodyConstraints.FreezeRotation; //freeze the x and z position of the item
            item.transform.localScale = item.GetComponent<Item>().PlacedScale; //change the scale of the item

            item.transform.parent = GameObject.Find("Level").transform; //remove the item from the player
            item.transform.position = gameObject.transform.position + transform.forward * 2.5f; //move the item to the players forward position
            controller.radius = 0.5f;
            heldItems--; //decrement held items
        }
    }
}
