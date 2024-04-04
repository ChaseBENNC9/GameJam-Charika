/*
* Description: This is a child class of Item Goal and manages changed methods for complex type goals.
* Author: Chase Bennett-Hill
* Last Modified: 28 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/

using System.Collections.Generic;
using UnityEngine;

public class ComplexGoal : ItemGoal
{
    public List<GameObject> items;
    public int itemsNeeded; //could become const

    void Start()
    {
        isComplex = true;
        items = new List<GameObject>();
    }

    public override bool CanUseObject(GameObject item) 
    {
        Item i = item.GetComponent<Item>();

        if (items.Count < itemsNeeded && i.Type.ToLower() == itemName.ToLower() && i.priority == items.Count + 1) //if the item is not already in the list and the item is the correct type
        {
            ShowHint(true, hintAction);

            return true;
        }
        else
        {
            if (i.priority != items.Count + 1)
            {
                ShowHint(true, "Something is needed before \n this item can be placed here");
            }
            if (i.Type.ToLower() != itemName.ToLower())
            {
                ShowHint(true, "This item cannot be used here");
            }
            if (items.Count >= itemsNeeded)
            {
                ShowHint(true, "No more items are needed here");
            }

            return false;
        }
    }

    public override void UseObject(GameObject item = null) // This method will check if the goal has all of its required items and either run the goal action or add the held item to the list
    {
        Debug.Log("Item used complex in " + gameObject.name);
        if (items.Count == itemsNeeded && item == null)  //if the goal has all of its required items and nothing was passed in for the item
        {
            goalAction.Invoke(); //Calls the action for the goal
            print("Goal completed");
        }

        else
        {
            print("Item added to list");
            items.Add(item); //add the item to the list
        }
    }
}
