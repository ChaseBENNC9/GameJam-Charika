using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AnimationType
{
    Move,
    Rotate
}

public class AnimateObject : MonoBehaviour
{
    public bool isAnimating = false;
    public AnimationType animationType;
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
            switch (animationType)
            {
                case AnimationType.Move:
                    StartCoroutine(InterpolatePosition(transform.position, target, duration));
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
                default:
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
            transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / duration
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
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
