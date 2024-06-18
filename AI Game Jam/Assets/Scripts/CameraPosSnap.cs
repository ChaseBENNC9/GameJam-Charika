using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosSnap : MonoBehaviour
{
    // GameObjects
    [SerializeField] public GameObject cam;
    [SerializeField] private List<GameObject> hideObjects = new List<GameObject>(); // walls that have children with mesh renderers
    [SerializeField] private List<GameObject> exceptionWalls = new List<GameObject>(); // walls that have a mesh renderer attached
    [SerializeField] private List<GameObject> library = new List<GameObject>();
    
    // Booleans
    private bool hideWalls;

    // Consts
    private const float FIRST_FLOOR_Y = 7.09f;
    private const float FLOOR_Z = -24.24f;
    private const float SECOND_FLOOR_Y = 17.47f;
    private const float BEDROOM_Z = 2.85f;
    private const float CELLAR_Z = -10f;
    private const float CELLAR_Y = -0.6f;

    private const float OUTDOORS_Y = 6.7f;
    private const float OUTDOORS_Z = -21.1f;

    public void OnTriggerEnter(Collider other)
    {
        // Changes camera position when player enters or exits the house
        if (other.gameObject.tag == "FirstFloor")
        {
            cam.transform.position = new Vector3(transform.position.x, FIRST_FLOOR_Y, FLOOR_Z);
                    cam.GetComponent<ImprovedCameraFollow>().yValue = FIRST_FLOOR_Y;
                cam.GetComponent<ImprovedCameraFollow>().allowCameraMovement = true;
        }
        
        // Changes camera position when player is in the second floor hallway
        if (other.gameObject.tag == "SecondFloor")
        {
            cam.transform.position = new Vector3(transform.position.x, SECOND_FLOOR_Y, FLOOR_Z);
            cam.GetComponent<ImprovedCameraFollow>().yValue = SECOND_FLOOR_Y;
        }

        if (other.gameObject.name == "Camera Bedroom")
        {
            cam.transform.position = new Vector3(transform.position.x, SECOND_FLOOR_Y, BEDROOM_Z);
            cam.GetComponent<ImprovedCameraFollow>().yValue = SECOND_FLOOR_Y;
            for (int i = 0; i < library.Count; i++)
            {
                library[i].SetActive(false);
            }
        }  

        if (other.gameObject.name == "Cellar Stairs")
        {
            cam.transform.position = new Vector3(transform.position.x, -2.52f, CELLAR_Z);
            cam.GetComponent<ImprovedCameraFollow>().allowCameraMovement = false;
            cam.GetComponent<ImprovedCameraFollow>().yValue = -2.52f;
            cam.GetComponent<ImprovedCameraFollow>().xValue = 74.49f;
            cam.GetComponent<ImprovedCameraFollow>().zValue = -10f;
        }      
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Camera Stairs")
        {
            cam.transform.position = new Vector3(transform.position.x, this.transform.position.y, CELLAR_Z);
            cam.GetComponent<ImprovedCameraFollow>().yValue = this.transform.position.y;
        }

        // Hides walls when player is in the second floor hallway
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
        if (other.gameObject.tag == "FirstFloor")
        {
            LeaveHouse();
        }

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

        // Changes camera position when player is in the bedroom
        else if (other.gameObject.name == "Camera Bedroom")
        {
            cam.transform.position = new Vector3(transform.position.x, SECOND_FLOOR_Y, FLOOR_Z);
            for (int i = 0; i < library.Count; i++)
            {
                library[i].SetActive(true);
            }
        }
    }


    public void LeaveHouse()
    {
        cam.transform.position = new Vector3(transform.position.x, OUTDOORS_Y, OUTDOORS_Z);
        cam.GetComponent<ImprovedCameraFollow>().yValue = OUTDOORS_Y;
        Debug.Log("Player has left the house");
        Debug.Log("Camera position: " + cam.transform.position);
    }
}
