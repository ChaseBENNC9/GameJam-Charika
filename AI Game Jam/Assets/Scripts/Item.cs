using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private string type;
    private Vector3 placedScale;
    public Vector3 PlacedScale { get { return placedScale; } }
    public Vector3 heldScale;


    void Start()
    {
        placedScale = transform.localScale;
    }
    public string GetItemType()
    {
        return type;
    }

}
