using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Description: Allows the grass to always face the camera
Author: Erika Stuart
*/

public class BillBoard : MonoBehaviour
{
    private GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform.position, Vector3.up);
    }
}
