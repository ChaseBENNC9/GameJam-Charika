using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityLight : MonoBehaviour
{
    private Vector3 min;
    private Vector3 max;
    // Start is called before the first frame update
    void Start()
    {
        min = new Vector3(-20, gameObject.transform.parent.position.y, -20);
        max = new Vector3(20, gameObject.transform.parent.position.y, 20);
        StartCoroutine(moveLight());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Game Over");
        }
    }

    private IEnumerator moveLight()
    {
        while (true)
        {
            //gameObject.transform.parent.position = new Vector3.MoveTowards(Random.Range(min.x, max.x), gameObject.transform.parent.position.y, Random.Range(min.z, max.z));
            gameObject.transform.parent.position = Vector3.MoveTowards(gameObject.transform.parent.position, new Vector3(Random.Range(min.x, max.x), gameObject.transform.parent.position.y, Random.Range(min.z, max.z)), 1*Time.deltaTime);
            yield return new WaitForSeconds(2);
        }
    }
}
