using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{
    private bool startAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveDown()
    {

        if (transform.position.y > -14)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - 0.1f,
                transform.position.z
            );
            StartCoroutine(Delay(1));
        }

    }
    public void OpenDoor()
    {

        if (transform.rotation.y < 65)
        {
            print("this should say true once");
                transform.rotation = Quaternion.Euler(
                transform.position.x,
                transform.position.y + 0.5f,
                transform.position.z
            );
            StartCoroutine(Delay(1));
        }

    }


    private IEnumerator Delay(int seconds)
    {
        yield return new WaitForSeconds(1);
    }
}



