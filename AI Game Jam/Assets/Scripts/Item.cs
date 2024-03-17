/*
* Description: Manages the Item and sets the scale of the item when it is held and when it is placed
* Author: Chase Bennett-Hill
* Last Modified: 15 / 03 / 24
* Last Modified By: Chase Bennett-Hill
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string type; //This is the type of item it is eg key, block, axe
    public string Type
    {
        get => type;
    } //Creates a get for the type so other scripts can read its value
    private Vector3 placedScale; //this is the scale that the object should be when it is placed in the world
    public Vector3 PlacedScale
    {
        get => placedScale;
    } //Creates a get for the placed scale so other scripts can read its value

    public Vector3 HeldScale
    {
        get => heldScale;
    } //Creates a get for the held scale so other scripts can read its value

    [SerializeField]
    private Vector3 heldScale; //this is the scale that the object should be when it is held by the player

    void Start()
    {
        placedScale = transform.localScale;
    }
}
