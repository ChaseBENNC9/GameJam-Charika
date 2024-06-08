using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosSnap : MonoBehaviour
{
    // First Floor
    // y = 7.09
    // z = -24.24
    // Second Floor
    // y = 17.47
    // z = -24.24

    [SerializeField] private GameObject cam;
    private bool isIndoors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FirstFloor" && !isIndoors)
        {
            isIndoors = true;
            cam.transform.position = new Vector3(transform.position.x, 7.09f, -24.24f);
            Debug.Log("Indoors");
        }
        else if (other.gameObject.tag == "SecondFloor")
        {
            cam.transform.position = new Vector3(transform.position.x, 17.47f, -24.24f);
        }
        else if (other.gameObject.tag == "FirstFloor" && isIndoors)
        {
            isIndoors = false;
            cam.transform.position = new Vector3(transform.position.x, 5.7f, -29.1f);
            Debug.Log("Outdoors");
        }
    }
}
