/*
* Description: This is used to manage details of the level such as checkpoints and respawning the player at the active checkpoint.
* Author: Chase Bennett-Hill
* Last Modified: 20 / 03 / 2024
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private List<Checkpoint> checkpoints;
    private Checkpoint activeCheckpoint;

    [SerializeField]
    private GameObject playerprefab; //the player prefab used to respawn the player
    public static LevelManager instance; //creates an instance of the level manager so that it can be accessed from other scripts
    private const float PLAYERY = 1.08f; // the y value of the player object

    // Start is called before the first frame update



    void Start()
    {
        instance = this;
        checkpoints = new List<Checkpoint>();
        foreach (Checkpoint checkpoint in FindObjectsOfType<Checkpoint>()) //iterates through a list of all the checkpoints in the scene
        {
            checkpoints.Add(checkpoint); //adds the checkpoint to the list of checkpoints
        }
        if (activeCheckpoint == null) //if there is no active checkpoint, the active checkpoint is set to the level start
        {
            activeCheckpoint = GameObject.Find("LevelStart").GetComponent<Checkpoint>();
        }
    }

    public void SetActiveCheckpoint(Checkpoint checkpoint) //sets the active checkpoint to the checkpoint passed in and deactivates all other checkpoints
    {
        foreach (Checkpoint cp in checkpoints)
        {
            cp.isActive = false;
        }
        checkpoint.isActive = true;
        activeCheckpoint = checkpoint;
    }

    // Update is called once per frame


    public void Respawn() //respawns the player at the active checkpoint
    {
        GameObject player = GameObject.Find("Player"); //finds the player object and destroys it
        Destroy(player);
        GameObject respawnPlayer = GameObject.Instantiate(
            playerprefab,
            new Vector3(activeCheckpoint.location.x, PLAYERY, activeCheckpoint.location.z),
            Quaternion.identity //respawns the player at the active checkpoint
        );
        respawnPlayer.name = "Player"; //sets the name and tag of the player object to "Player"
        respawnPlayer.tag = "Player";
        FindAnyObjectByType<CameraFollow>().Player = respawnPlayer; //finds the camera follow script and sets the player to the respawned player
    }
}
