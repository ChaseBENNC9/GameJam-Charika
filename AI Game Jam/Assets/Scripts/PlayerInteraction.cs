/*
* Description: This script is used for player interactions and manages picking up and dropping items
* Author: Erika Stuart
* Last Modified: 15 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/


using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool inRange,
        inRangeGoal;
    [HideInInspector] public GameObject item;
    public GameObject heldItem;
    private Vector3 itemScale;
    public GameObject itemGoal;
    private CharacterController controller; //Character controller component
    public int heldItems;
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
            if (item != null && item.GetComponent<Item>().Type.ToLower() == other.gameObject.transform.parent.GetComponent<ItemGoal>().itemName.ToLower()) //if the item name is the same as the goal name
            {
                other.gameObject.transform.parent
                .GetComponent<ItemGoal>().ShowHint(true, other.gameObject.transform.parent
                .GetComponent<ItemGoal>().hintWithItem); //show the hint for the goal wnen the player has the item
            }
            else
            {
                other.gameObject.transform.parent.GetComponent<ItemGoal>()
                .GetComponent<ItemGoal>().ShowHint(true, other.gameObject.transform.parent
                .GetComponent<ItemGoal>().hintNoItem); //show the hint for the goal when the player does not have the item
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Item")
        {
            inRange = false;
            item = null;
        }
        else if (collider.gameObject.tag == "ItemGoal")
        {
            inRangeGoal = false;
            itemGoal = null;
            //  itemGoal.GetComponent<ItemGoal>().ShowHint(false);
        }
        if (collider.gameObject.tag == "hintCollider")
        {
            collider.gameObject.transform.parent.GetComponent<ItemGoal>().ShowHint(false);
        }
    }

    private void PickUp()
    {
        //happens only once/when key press
        if (inRange && Input.GetKeyDown(KeyCode.Mouse0) && heldItems < MAXITEMS && item != null) //if in range and clicks mouse button and not currently holding anything
        {
            Debug.Log("Picking up item");
            if (item.TryGetComponent<Rigidbody>(out var rb)) //if the item has a rigidbody
            {
                Destroy(rb); //destroy the rigidbody
            }
            heldItem = item; //assign the heldItem as the item
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
                UseObjectAtGoal(itemGoal.GetComponent<ItemGoal>().IsComplex);
            }
            else
            {
                DropObject();
            }
        }
        else if (heldItems == 0 && inRangeGoal && itemGoal.GetComponent<ItemGoal>().IsComplex && Input.GetKeyDown(KeyCode.E)
        )
        {
            itemGoal.GetComponent<ComplexGoal>().UseObject();
        }
    }

    private void UseObjectAtGoal(bool complex)
    {
        if (complex)
        {
            if (Input.GetKeyDown(KeyCode.E) && itemGoal.GetComponent<ComplexGoal>().CanUseObject(heldItem)) //if in range and clicks mouse button
            {

                heldItem.transform.rotation = Quaternion.Euler(0, 0, 0); //reset the rotation of the item
                heldItem.transform.rotation = itemGoal.transform.rotation; //rotate the item to the rotation of the goal
                heldItem.transform.localScale = heldItem.GetComponent<Item>().PlacedScale; //change the scale of the item
                heldItem.transform.parent = itemGoal.transform; //remove the item from the player
                heldItem.transform.position = itemGoal.transform.position; //move the item to the position of the goal
                heldItem.transform.localPosition = heldItem.GetComponent<Item>().PlacedPosition; //move the heldItem to the local position of the goal
                heldItem.transform.localRotation = Quaternion.Euler(heldItem.GetComponent<Item>().PlaceRotation); //rotate the item to the rotation of the goal
                if (item.TryGetComponent<BoxCollider>(out var rb))
                {
                    Destroy(rb); //destroy the rigidbody
                }

                itemGoal.GetComponent<ComplexGoal>().UseObject(heldItem);


                controller.radius = 0.5f;
                heldItems--; //decrement held items
                Debug.Log("Using itemxGOAL");
                heldItem = null;
                item = null;
            }
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.E) && itemGoal.GetComponent<ItemGoal>().CanUseObject(heldItem)) //if in range and clicks mouse button
            {

                heldItem.transform.rotation = Quaternion.Euler(0, 0, 0); //reset the rotation of the item
                heldItem.transform.rotation = itemGoal.transform.rotation; //rotate the item to the rotation of the goal
                heldItem.transform.localScale = heldItem.GetComponent<Item>().PlacedScale; //change the scale of the item
                heldItem.transform.parent = itemGoal.transform; //remove the item from the player
                heldItem.transform.position = itemGoal.transform.position; //move the item to the position of the goal
                heldItem.transform.localPosition = heldItem.GetComponent<Item>().PlacedPosition; //move the heldItem to the local position of the goal
                heldItem.transform.localRotation = Quaternion.Euler(heldItem.GetComponent<Item>().PlaceRotation); //rotate the item to the rotation of the goal
                if (item.TryGetComponent<BoxCollider>(out var rb))
                {
                    Destroy(rb); //destroy the rigidbody
                }
                if (itemGoal.GetComponent<ItemGoal>().IsComplex)
                {
                    itemGoal.GetComponent<ComplexGoal>().UseObject(heldItem);
                }
                else
                {
                    itemGoal.GetComponent<ItemGoal>().UseObject(heldItem);
                }
                controller.radius = 0.5f;
                heldItems--; //decrement held items
                Debug.Log("Using itemxGOAL");
                heldItem = null;
                item = null;
            }
        }
    }

    private void DropObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) //if in range and clicks mouse button
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
            Debug.Log("Using itemxDROP");
            heldItem = null;
            item = null;
        }
    }
}
