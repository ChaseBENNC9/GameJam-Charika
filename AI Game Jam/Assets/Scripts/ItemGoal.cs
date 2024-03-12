using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemGoal : MonoBehaviour
{
    public string itemName; //what item can be placed here
    public GameObject hint; //the item that can be placed here
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseObject()
    {
       if(gameObject.name == "door")
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
        Debug.Log("You can place " + itemName + " here");
        hint.GetComponent<TMP_Text>().text = hintstring;
        hint.SetActive(show);
    }

}
