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
        if (startAnimation)
        {
            if (transform.position.y > -14)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y - 0.1f,
                    transform.position.z
                );
                StartCoroutine(MoveDownCoroutine());
            }
        }
    }

    public void MoveDown()
    {
        startAnimation = true;
    }

    private IEnumerator MoveDownCoroutine()
    {
        yield return new WaitForSeconds(1);
    }
}
