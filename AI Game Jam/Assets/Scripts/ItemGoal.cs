using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGoal : MonoBehaviour
{
    public string itemName; //what item can be placed here
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
}
