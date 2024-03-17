/*
* Description: This script is used to manage the area where the player can place items. It also shows hints for the goals when the player is in range of the goal.
* Author: Chase Bennett-Hill
* Last Modified: 13 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemGoal : MonoBehaviour
{
    public string itemName; //what item can be placed here
    
    public GameObject hint; //the item that can be placed here
    // Start is called before the first frame update
    public string hintNoItem = ""; //initalizes the hints as empty
    public string hintWithItem = "";

    public void UseObject()
    {
       if(gameObject.name.Contains("Door")) //if the objects name contains Door it will destroy itself
        {
            GameObject.Destroy(gameObject); 
        }
        else
        {
            Debug.Log("Item placed in " + gameObject.name);
        }
    }

    public void ShowHint(bool show, string hintstring = "")
    {
        hint.GetComponent<TMP_Text>().text = hintstring;
        hint.SetActive(show);
    }

}
