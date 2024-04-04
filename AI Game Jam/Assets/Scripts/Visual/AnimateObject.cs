using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{

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
    // public void OpenDoor()
    // {

    //     if (transform.localRotation.y < 65)
    //     {
    //         print(transform.localRotation.y);
    //             transform.localRotation = Quaternion.Euler(
    //             0,
    //              transform.localRotation.y+ 20f,
    //             0
    //         );
    //         StartCoroutine(Delay(1));
    //         print(transform.localRotation.y);
    //     }
    //     {
    //         print("exit");
    //     }

    // }


    private IEnumerator Delay(int seconds)
    {
        yield return new WaitForSeconds(1);
    }
}



