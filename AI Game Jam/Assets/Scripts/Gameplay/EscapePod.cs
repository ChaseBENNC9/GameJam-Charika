using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
Description: Allows the player to escape the level when they reach the escape pod
Author: Chase Bennett-Hill
*/

public class EscapePod : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inRange;

    void Start()
    {
        inRange = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
            LevelManager.instance.hintTitle.text = "Escape Pod";
            LevelManager.instance.hintContent.text = "Press E to escape";
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            LevelManager.instance.hintTitle.text = "";
            LevelManager.instance.hintContent.text = "";
        }
    }
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            GameSettings.Level = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
        }
    }
}
