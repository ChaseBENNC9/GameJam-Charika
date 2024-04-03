/*
* Description: This script is used to manage the area where the player can place items.
It also shows hints for the goals when the player is in range of the goal. This class is a parent of the ComplexGoal class
* Author: Chase Bennett-Hill
* Last Modified: 28 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/

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

    [SerializeField]
    protected UnityEvent goalAction; //the action will be called when the goal is interacted with and its requirements are met protected so it can be accessed by child classes

    [SerializeField] protected bool isComplex; //if the hint should be shown protected so it can be accessed by child classes
    public bool IsComplex
    {
        get => isComplex;
    } //Creates a get for the isComplex so other scripts can read its value

    public bool CanUseObject(GameObject item)
    {
        ShowHint(true, hintAction);
        return true;
    }

    public void UseObject(GameObject item)
    {
        Debug.Log("Item used in " + gameObject.name);

        goalAction.Invoke(); //Calls the action for the goal
    }

    public void ShowHint(bool show, string hintstring = "")
    {
        hint.GetComponent<TMP_Text>().text = hintstring;
        hint.SetActive(show);
    }

    public void GoalActionChildBall() //This method will be run by the goal action when the goal is completed if it is set in the inspector
    {
        hintNoItem = "";
    }

    public void GoalActionOpenDoor()
    {

        Destroy(gameObject);
    }
}
