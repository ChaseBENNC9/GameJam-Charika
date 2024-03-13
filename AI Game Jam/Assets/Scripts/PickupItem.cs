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
    public bool inRange , inRangeGoal;
    public GameObject item;
    public GameObject itemGoal;

    private int heldItems;
    private const int MAXITEMS = 1;

    // Update is called once per frame
    void Start()
    {
        heldItems = 0;
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
            item.transform.parent = gameObject.transform ; //stick to players position
            item.transform.position = gameObject.transform.position + transform.forward * 2; //move the item to the players forward position
            heldItems++; //increment held items
        }
        else if (inRangeGoal && Input.GetKeyDown(KeyCode.Mouse0) && heldItems > 0 && item.gameObject.name == itemGoal.GetComponent<ItemGoal>().itemName) //if in range of goal and clicks mouse button and holding an item with the correct name
        {
            item.transform.parent = itemGoal.transform ; //remove the item from the player
            item.transform.position = itemGoal.transform.position ;//move the item to the position of the goal
            heldItems--; //decrement held items
            itemGoal.GetComponent<ItemGoal>().UseObject();
        }
    }
}
