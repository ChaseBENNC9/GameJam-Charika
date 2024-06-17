/*
* Description: This script is used for player interactions and manages picking up and dropping items
* Author: Erika Stuart
* Last Modified: 15 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/


using System;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private const string ITEM_TAG = "Item";
    private const string ITEM_GOAL_TAG = "ItemGoal";
    private const string HINT_COLLIDER_TAG = "hintCollider";
    private const KeyCode KEY_DROP = KeyCode.Mouse1;
    private const KeyCode KEY_PICKUP = KeyCode.Mouse0;
    private const KeyCode KEY_USE = KeyCode.E;
    private const int MAX_ITEMS = 1;


    private bool inRange;
    private bool inRangeGoal;
    private GameObject closestItem;
    [HideInInspector] public GameObject heldItem;
    [HideInInspector] public GameObject itemGoal;

    private CharacterController controller; //Character controller component
    public int heldItems;

    // Update is called once per frame
    void Start()
    {
        heldItems = 0;
        controller = gameObject.GetComponent<CharacterController>(); //Gets the character controller component
    }

    void Update()
    {
        PickUp();
        UseObject();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ITEM_TAG) && other.gameObject != heldItem)
        {
            inRange = true;
            closestItem = other.gameObject;
            if (!heldItem)
            {
                LevelManager.instance.hintTitle.text = closestItem.gameObject.name;
                LevelManager.instance.hintContent.text = "Press Left Mouse Button to pick up";
            }
            else
            {
                LevelManager.instance.hintTitle.text = closestItem.gameObject.name;
                LevelManager.instance.hintContent.text = "Already holding " + heldItem.gameObject.name;
            }

        }
        else if (other.CompareTag(ITEM_GOAL_TAG))
        {
            inRangeGoal = true;
            itemGoal = other.gameObject;
            ItemGoal currentGoal = itemGoal.GetComponent<ItemGoal>();
        }
        else if (other.CompareTag(HINT_COLLIDER_TAG))
        {
            ItemGoal goal = other.gameObject.transform.parent.GetComponent<ItemGoal>();
            if (heldItem != null && heldItem.GetComponent<Item>().Type == goal.itemName)
            {
                goal.ShowHint(true, goal.hintWithItem);
            }
            else
            {
                goal.ShowHint(true, goal.hintNoItem);
            }


        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(ITEM_TAG))
        {
            LevelManager.instance.hintTitle.text = "";
            LevelManager.instance.hintContent.text = "";
            if (heldItem != null)
            {
                LevelManager.instance.hintTitle.text = heldItem.name;
                LevelManager.instance.hintContent.text = "Press Right Mouse Button to drop item";
            }
            inRange = false;
            closestItem = null;
        }
        else if (collider.CompareTag(ITEM_GOAL_TAG))
        {
            inRangeGoal = false;
            itemGoal = null;
            //  itemGoal.GetComponent<ItemGoal>().ShowHint(false);
        }
        if (collider.CompareTag(HINT_COLLIDER_TAG))
        {
            collider.gameObject.transform.parent.GetComponent<ItemGoal>().ShowHint(false);
            LevelManager.instance.hintContent.text = "";
            LevelManager.instance.hintTitle.text = "";
        }
    }

    private void PickUp()
    {
        //happens only once/when key press
        if (inRange && Input.GetKeyDown(KEY_PICKUP) && heldItems < MAX_ITEMS && closestItem != null) //if in range and clicks mouse button and not currently holding anything
        {
            heldItem = closestItem; //assign the heldItem as the item
            closestItem = null; //Clears the value of item
            LevelManager.instance.hintTitle.text = heldItem.name;
            LevelManager.instance.holdingText.text = heldItem.name;
            LevelManager.instance.hintContent.text = "Press Right Mouse Button to drop item";
            if (heldItem.TryGetComponent<Rigidbody>(out var rb)) //if the item has a rigidbody
            {
                Destroy(rb); //destroy the rigidbody
            }


            heldItem.transform.parent = gameObject.transform; //stick to players position
            heldItem.transform.localRotation = Quaternion.Euler(0, 0, 0); //reset the rotation of the item

            heldItem.transform.position = gameObject.transform.position + transform.forward * 0.7f; //move the item to the players forward position
            heldItems++; //increment held items
            heldItem.transform.localScale = heldItem.GetComponent<Item>().HeldScale; //change the scale of the item
            controller.radius = 1.0f;
        }
    }

    private void UseObject()
    {
        if (heldItems > 0 && heldItem != null)
        {

            if (inRangeGoal) //if in range of goal and clicks mouse button and holding an item with the correct name
            {
                UseObjectAtGoal();

            }
            else
            {
                DropObject();
            }
        }
        
        else if ((heldItems == 0 || (heldItems > 0 && heldItem.name != itemGoal.GetComponent<ItemGoal>().itemName)) && inRangeGoal && itemGoal.GetComponent<ItemGoal>().IsComplex && Input.GetKeyDown(KEY_USE))
        {
            itemGoal.GetComponent<ComplexGoal>().UseObject();

        }
        
    }

    private void UseObjectAtGoal()
    {
        if (Input.GetKeyDown(KEY_USE) && itemGoal.GetComponent<ItemGoal>().CanUseObject(heldItem)) //if in range and clicks mouse button
        {

            LevelManager.instance.hintTitle.text = "";
            LevelManager.instance.hintContent.text = "";
            heldItem.transform.rotation = Quaternion.Euler(0, 0, 0); //reset the rotation of the item
            heldItem.transform.rotation = itemGoal.transform.rotation; //rotate the item to the rotation of the goal
            heldItem.transform.localScale = heldItem.GetComponent<Item>().PlacedScale; //change the scale of the item
            heldItem.transform.parent = itemGoal.transform; //remove the item from the player
            heldItem.transform.position = itemGoal.transform.position; //move the item to the position of the goal
            heldItem.transform.localPosition = heldItem.GetComponent<Item>().PlacedPosition; //move the heldItem to the local position of the goal
            heldItem.transform.localRotation = Quaternion.Euler(heldItem.GetComponent<Item>().PlaceRotation); //rotate the item to the rotation of the goal
            if (heldItem.TryGetComponent<BoxCollider>(out var rb))
            {
                Destroy(rb); //destroy the rigidbody
            }

            itemGoal.GetComponent<ItemGoal>().UseObject(heldItem);

            controller.radius = 0.5f;
            heldItems--; //decrement held items
            heldItem = null;
            closestItem = null;
        }
    }

    private void DropObject()
    {
        if (Input.GetKeyDown(KEY_DROP)) //if in range and clicks mouse button
        {
            Rigidbody rb = heldItem.AddComponent<Rigidbody>(); //add a rigidbody to the item
            rb.constraints =
                RigidbodyConstraints.FreezePositionX
                | RigidbodyConstraints.FreezePositionZ
                | RigidbodyConstraints.FreezeRotation; //freeze the x and z position of the item
            heldItem.transform.localScale = heldItem.GetComponent<Item>().PlacedScale; //change the scale of the item

            heldItem.transform.parent = GameObject.Find("Level").transform; //remove the item from the player
            heldItem.transform.position = gameObject.transform.position + transform.forward * 2.5f; //move the item to the players forward position
            controller.radius = 0.5f;
            heldItems--; //decrement held items
            heldItem = null;
            closestItem = null;

            LevelManager.instance.holdingText.text = "";
        }
    }
}
