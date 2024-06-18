/*
* Description: Manages the Camera Follow, sets the camera to follow the player
* Author: Chase Bennett-Hill
* Last Modified: 07 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/

using UnityEngine;

public class ImprovedCameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player; //sets the player as the object to follow
    private Vector3 offset = new Vector3(); //sets the offset of the camera

    public GameObject Player { get => player; set => player = value; }

    public int maxZvalue = -30;
    [HideInInspector] public float yValue;
    
    [HideInInspector] public float xValue;
    [HideInInspector] public float zValue;


    public bool allowCameraMovement = true;

    void Start()
    {
        yValue = transform.position.y;
        xValue = transform.position.x;
        zValue = transform.position.z;
        offset = new Vector3(0,10f,-20f); //The Camera will be 2.5 units above the player (Y) and 7 units behind the player (Z)
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(allowCameraMovement)
        {
            if(Player.transform.position.z > maxZvalue)
                transform.position = new Vector3(Player.transform.position.x + offset.x, yValue, Player.transform.position.z + offset.z); //sets the camera to follow the player
            else
                transform.position = new Vector3(Player.transform.position.x + offset.x, yValue, -56); //sets the camera to follow the player
        }
        else
        {
            transform.position = new Vector3(xValue, yValue, zValue);
        }
        
    }
}
