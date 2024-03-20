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
    [SerializeField] private List<Checkpoint> checkpoints;
    private Checkpoint activeCheckpoint;
  [SerializeField] private GameObject playerprefab;
    public static LevelManager instance;

    // Start is called before the first frame update



    void Start()
    {
        instance = this;
        checkpoints = new List<Checkpoint>();
        foreach (Checkpoint checkpoint in FindObjectsOfType<Checkpoint>())
        {
            checkpoints.Add(checkpoint);
        }
        if (activeCheckpoint == null)
        {
            activeCheckpoint = GameObject.Find("LevelStart").GetComponent<Checkpoint>();
        }
    }

    public void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        foreach (Checkpoint cp in checkpoints)
        {
            cp.isActive = false;
        }
        checkpoint.isActive = true;
        activeCheckpoint = checkpoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        GameObject player = GameObject.Find("Player");
        Destroy(player);
            GameObject respawnPlayer = GameObject.Instantiate(
                playerprefab,
                new Vector3(activeCheckpoint.location.x, 1.08f, activeCheckpoint.location.z),
                Quaternion.identity
            );
            respawnPlayer.name = "Player";
            respawnPlayer.tag = "Player";
            FindAnyObjectByType<CameraFollow>().Player = respawnPlayer;
    }
    
}
