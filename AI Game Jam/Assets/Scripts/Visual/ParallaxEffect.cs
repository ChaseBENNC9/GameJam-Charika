
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startPos;
    private float imageLength;
    [SerializeField]private float speed;
    public Camera mainCam;
    private float temp;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        imageLength = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground();
        RepeatingBackground();
    }

    private void MoveBackground()
    {
        Vector3 camPos = mainCam.transform.position; //sets the cameras position
        temp = camPos.x * (1 - speed); //1 is same speed as camera, so a temp value is stored based on the current position of the camera and how much less the speed is to the cameras speed
        distance = camPos.x * speed; //calculates the distance between the camera and speed

        Vector3 newPos = new Vector3(startPos + distance, transform.position.y, transform.position.z); //calculates the new position of the layer
        transform.position = newPos;   
    }

    private void RepeatingBackground()
    {
        if (temp > startPos + (imageLength/2)) // if the camera gets to half the length of the layer then it will repeat
        {
            startPos += imageLength;
        }
        else if (temp < startPos - (imageLength/2))
        {
            startPos -= imageLength;
        }
    }
}
