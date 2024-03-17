using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityLightMovement : MonoBehaviour
{
    private Vector3 min;
    private Vector3 max;
    private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        min = new Vector3(-20, 12, -20);
        max = new Vector3(20, 12, 20);
        targetPos = new Vector3(0, 12, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position == targetPos)
        {
            targetPos = new Vector3(Random.Range(min.x, max.x), 12, Random.Range(min.z, max.z));
            StartCoroutine(moveLight());
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, 12f * Time.deltaTime);
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