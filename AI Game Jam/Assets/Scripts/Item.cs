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

    [SerializeField] private string type;
    private Vector3 placedScale;
    public Vector3 PlacedScale { get => placedScale; }

    public Vector3 HeldScale { get => heldScale; }

    private Vector3 heldScale;


    void Start()
    {
        placedScale = transform.localScale;
    }
    public string GetItemType()
    {
        return type;
    }

}
