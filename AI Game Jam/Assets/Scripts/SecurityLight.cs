using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityLight : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Game Over");
        }
    }
}
