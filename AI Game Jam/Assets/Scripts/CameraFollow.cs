/*
* Description: Manages the Camera Follow, sets the camera to follow the player
* Author: Chase Bennett-Hill
* Last Modified: 7 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player; //sets the player as the object to follow
    private Vector3 offset = new Vector3(); //sets the offset of the camera
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0,2.5f,-7f); //The Camera will be 2.5 units above the player (Y) and 7 units behind the player (Z)
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z); //sets the camera to follow the player
    }
}
