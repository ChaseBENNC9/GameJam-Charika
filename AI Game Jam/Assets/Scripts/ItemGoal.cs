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

public class ItemGoal : MonoBehaviour
{
    public string itemName; //what item can be placed here
    public GameObject hint; //the item that can be placed here
    public string hintNoItem = ""; //initalizes the hints as empty
    public string hintWithItem = "";

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
                return true;
            }
            else
            {
                return false;
            }

             }
                else
                {
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
            if (gameObject.name.Contains("Door")) //if the objects name contains Door it will destroy itself
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                Debug.Log("Item placed in " + gameObject.name);
            }
        }
    }


    public void UseComplexPuzzle()
    {
        if (items.Count == itemsNeeded)
        {
            if (gameObject.name.Contains("Door")) //if the objects name contains Door it will destroy itself
            {
                GameObject.Destroy(gameObject);
            }

            if (gameObject.name.Contains("WheelGoal")) //if the objects name contains Lever it will destroy itself
            {
                Debug.Log("Wheel turned and water drained");
            }
        }

        
    }

    public void ShowHint(bool show, string hintstring = "")
    {
        hint.GetComponent<TMP_Text>().text = hintstring;
        hint.SetActive(show);
    }
}
