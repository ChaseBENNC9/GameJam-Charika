/*
* Description: This script is used to manage the area where the player can place items. It also shows hints for the goals when the player is in range of the goal.
* Author: Chase Bennett-Hill
* Last Modified: 13 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ItemGoal : MonoBehaviour
{
    public string itemName; //what item can be placed here
    public GameObject hint; //the item that can be placed here
    public string hintNoItem = ""; //initalizes the hints as empty
    public string hintWithItem = "";
    public string hintAction = "";
    [SerializeField] private UnityEvent goalAction; //the action will be called when the goal is interacted with and its requirements are met

    [SerializeField]
    private bool isComplex; //if the hint should be shown
    public bool IsComplex
    {
        get => isComplex;
    } //Creates a get for the isComplex so other scripts can read its value
    public List<GameObject> items;
    public int itemsNeeded; //could become const

    void Start()
    {
        if (isComplex)
            items = new List<GameObject>();
    }


    public bool CanUseObject(GameObject item)
    {
             if (isComplex)
             {
             Item i = item.GetComponent<Item>();

            if (items.Count < itemsNeeded && i.Type == itemName && i.priority == items.Count + 1 ) //if the item is not already in the list and the item is the correct type
            {
                ShowHint(true, hintAction);

                return true;
            }
            else
            {
                if(i.priority != items.Count + 1 )
                {
                    ShowHint(true, "Something is needed before \n this item can be placed here");
                }
                 if (i.Type != itemName)
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
                else
                {
                    ShowHint(true, hintAction);
                    return true;
                }
    }
    
    public void UseObject(GameObject item)
    {
        Debug.Log("Item used in " + gameObject.name);
        if (isComplex)
        {
    
                items.Add(item);
            
    
        }
        
        else
        {
            goalAction.Invoke(); //Calls the action for the goal

        }
    }


    public void UseComplexPuzzle()
    {
        if (items.Count == itemsNeeded)
        {
            goalAction.Invoke();
        }

        
    }

    public void ShowHint(bool show, string hintstring = "")
    {
        hint.GetComponent<TMP_Text>().text = hintstring;
        hint.SetActive(show);
    }

    public void GoalActionDrainWater() //This method will be run by the goal action when the goal is completed if it is set in the inspector
    {
        Debug.Log("Goal Action Invoked");
        Debug.Log("Wheel turned and water drained");
    }
    public void GoalActionOpenDoor()
    {
        Debug.Log("Goal Action Invoked");
        Debug.Log("Door opened");
        GameObject.Destroy(gameObject);

    }
}
