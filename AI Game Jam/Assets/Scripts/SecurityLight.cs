/*
Description: Triggers a game over when the player enters the light
Author: Erika Stuart
Last Modified: 18 / 03 / 2024
Last Modified By: Erika Stuart
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityLight : MonoBehaviour
{
    void OnTriggerEnter(Collider other) //the sphere child object of the light
    {
        if (other.tag == "Player") //if the player enters the trigger
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //loads the same scene
        }
    }
}
