using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
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
