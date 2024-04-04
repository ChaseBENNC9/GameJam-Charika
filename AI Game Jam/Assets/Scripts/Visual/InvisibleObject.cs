
using UnityEngine;

public class InvisibleObject : MonoBehaviour
{
    private float distance;
    private float minDist;
    [SerializeField] private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        minDist = 15.0f;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, cam.transform.position);
        if (distance < minDist)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
