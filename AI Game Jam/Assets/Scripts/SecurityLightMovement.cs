/*
Description: Moves the security light around inside of a range
Author: Erika Stuart
Last Modified: 18 / 03 / 2024
Last Modified By: Erika Stuart
*/
using System.Collections;
using UnityEngine;

public class SecurityLightMovement : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 range; //the area the light can move in
    private Vector3 targetPos; //the position the light is moving to
    private const int lightHeight = 12; //the height of the light
    private const int rangeValue = 40; //the value for the range

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position; //get the current position of the light
        range = new Vector3(rangeValue, lightHeight, rangeValue); //set the range
        targetPos = new Vector3(startPos.x, lightHeight, startPos.z); //initial target so the light doesn't fall through floor
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position == targetPos) //if the light has reached its target
        {
            targetPos = new Vector3(Random.Range(startPos.x-range.x, startPos.x+range.x), lightHeight, Random.Range(startPos.z-range.z, startPos.z+range.z)); //set a new target
            StartCoroutine(moveLight()); //wait 2 seconds
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, lightHeight * Time.deltaTime); //move the light
        }
    }

    private IEnumerator moveLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
        }
    }
}