/*
* Description: Manages the Camera Follow, sets the camera to follow the player
* Author: Chase Bennett-Hill
* Last Modified: 07 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player; //sets the player as the object to follow
    private Vector3 offset = new Vector3(); //sets the offset of the camera

    public GameObject Player { get => player; set => player = value; }


    void Start()
    {
        offset = new Vector3(0,10f,-20f); //The Camera will be 2.5 units above the player (Y) and 7 units behind the player (Z)
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x + offset.x, Player.transform.position.y + offset.y, Player.transform.position.z + offset.z); //sets the camera to follow the player
    }
}
