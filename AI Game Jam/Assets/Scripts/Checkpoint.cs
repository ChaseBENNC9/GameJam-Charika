using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive;
    [SerializeField] public Vector3 location;
    // Start is called before the first frame update

       void Awake()
    {
                location = gameObject.transform.position;



    }
    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<LevelManager>().SetActiveCheckpoint(this);
        }
    }
}
