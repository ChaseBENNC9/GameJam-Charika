/*
* Description: This script is used to animate an object's position or rotation. The object will move or rotate to a target position or rotation over a specified duration.
* Author: Chase Bennett-Hill
* Last Modified: 8 / 04 / 24
* Last Modified By: Chase Bennett-Hill
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AnimationType //The type of animation that will be performed on the object
{
    Move,
    Rotate
}

public class AnimateObject : MonoBehaviour
{
    public bool isAnimating = false; //This is used to determine if the object is currently animating
    public AnimationType animationType; //This is the type of animation that will be performed on the object
    public Vector3 target; //This is the target position or rotation relative to the object's current position or rotation
    public float duration; //The time it takes to complete the animation

    public void StartAnimation()
    {
        isAnimating = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            switch (animationType) //Determine the type of animation that will be performed on the object based on the animationType variable
            {
                case AnimationType.Move:
                    StartCoroutine(InterpolatePosition(transform.position, transform.position+target, duration));
                    break;
                case AnimationType.Rotate:
                    StartCoroutine(
                        InterpolateRotation(
                            transform.localRotation,
                            Quaternion.Euler(target),
                            duration
                        )
                    );
                    break;
                default: //If an invalid animation type was selected, print an error message
                    Debug.Log("Error: Invalid animation type was selected");
                    break;
            }
        }
    }

    private IEnumerator InterpolatePosition(
        Vector3 startPosition,
        Vector3 targetPosition,
        float duration
    )
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / duration
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
        print("exit");
        isAnimating = false;
    }

    private IEnumerator InterpolateRotation(
        Quaternion startRotation,
        Quaternion targetRotation,
        float duration
    )
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.localRotation = Quaternion.Lerp(
                startRotation,
                targetRotation,
                elapsedTime / duration
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
        print("exit");
        isAnimating = false;
    }
}
