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
    private bool hideWalls;
    [SerializeField] private List<GameObject> hideObjects = new List<GameObject>(); // walls that have children with mesh renderers
    [SerializeField] private List<GameObject> exceptionWalls = new List<GameObject>(); // walls that have a mesh renderer attached

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
        // Changes camera position when player enters or exits the house
        if (other.gameObject.tag == "FirstFloor" && !isIndoors)
        {
            isIndoors = true;
            cam.transform.position = new Vector3(transform.position.x, 7.09f, -24.24f);
            //Debug.Log("Indoors");
        }
        else if (other.gameObject.tag == "FirstFloor" && isIndoors)
        {
            isIndoors = false;
            cam.transform.position = new Vector3(transform.position.x, 6.7f, -21.1f);
            //Debug.Log("Outdoors");
        }
        
        if (other.gameObject.tag == "SecondFloor")
        {
            cam.transform.position = new Vector3(transform.position.x, 17.47f, -24.24f);
            Debug.Log("Second Floor");
        }

        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Camera Stairs")
        {
            cam.transform.position = new Vector3(transform.position.x, this.transform.position.y, -24.24f);

        }

        if (other.gameObject.tag == "SecondFloor")
        {
            for (int i = 0; i < hideObjects.Count; i++)
            {
                foreach (Transform child in hideObjects[i].transform)
                {
                    child.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            for (int i = 0; i < exceptionWalls.Count; i++)
            {
                exceptionWalls[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SecondFloor")
        {
            for (int i = 0; i < hideObjects.Count; i++)
            {
                foreach (Transform child in hideObjects[i].transform)
                {
                    child.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            for (int i = 0; i < exceptionWalls.Count; i++)
            {
                exceptionWalls[i].GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
