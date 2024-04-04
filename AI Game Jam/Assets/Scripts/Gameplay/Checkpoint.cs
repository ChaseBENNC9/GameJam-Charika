/*
* Description: This is used to manage each checkpoint in the level, each checkpoint has a location and a bool to check if it is active.
* Author: Chase Bennett-Hill
* Last Modified: 20 / 03 / 2024
* 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive; //is the checkpoint the currently active checkpoint
    public Vector3 location; //the location of the checkpoint


    void Awake()
    {
        location = gameObject.transform.position; //sets the location of the checkpoint to the position of the checkpoint , used in awake so that it is set before the level manager starts
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //if the player enters the checkpoint it will tell levelmanager to set it as the active checkpoint
        {
            FindObjectOfType<LevelManager>().SetActiveCheckpoint(this);
        }
    }
}
