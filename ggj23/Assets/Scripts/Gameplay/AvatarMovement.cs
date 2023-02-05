using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement : MonoBehaviour
{
    [SerializeField] float minX, maxX;
    [SerializeField] float minIdleDuration, maxIdleDuration;
    [SerializeField] float moveSpeed;
    float duration;
    Vector3 moveToPosition;

    [SerializeField] Animator animator;

    Vector3 step;

    bool walking = false;

    bool animationShouldCange = true;


    private void Awake()
    {
        step = Vector3.right * moveSpeed;
    }


    void Update()
    {
        if (animationShouldCange)
        {
            animationShouldCange = false;

            if (walking)
            {
                duration = Random.Range(minIdleDuration, maxIdleDuration);
                //print("change to idling for " + duration);
                StartCoroutine(Idle());
            }
            else
            {
                moveToPosition = new Vector3(
                    Random.Range(minX, maxX),
                    transform.position.y,
                    0
                );
                //print("change to walking to " + moveToPosition);

                StartCoroutine(WalkTowards());
            }
        }
    }


    IEnumerator WalkTowards()
    {
        walking = true;
        bool walkingLeft = moveToPosition.x < transform.position.x;

        while ((moveToPosition.x < transform.position.x) == walkingLeft)
        {
            transform.position += (walkingLeft ? -step : step) * Time.deltaTime;
            yield return null;
        }

        animationShouldCange = true;
        yield return null;
    }


    IEnumerator Idle()
    {
        walking = false;

        yield return new WaitForSeconds(duration);

        animationShouldCange = true;
        yield return null;
    }
}