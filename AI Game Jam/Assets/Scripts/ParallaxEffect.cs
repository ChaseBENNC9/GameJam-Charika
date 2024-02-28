using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startPos;
    private float imageLength;
    [SerializeField]private float speed;
    public Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        imageLength = transform.localScale.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = mainCam.transform.position;
        float temp = camPos.x * (1 - speed);
        float distance = camPos.x * speed;

        Vector3 newPos = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        transform.position = newPos;   
    }
}
