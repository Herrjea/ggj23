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
    SpriteRenderer avatarRenderer;

    Vector3 step;

    bool walking = false;

    bool animationShouldCange = true;

    bool gameFinished = false;


    private void Awake()
    {
        step = Vector3.right * moveSpeed;

        avatarRenderer = animator.gameObject.GetComponent<SpriteRenderer>();

        GameEvents.P1Wins.AddListener(GameFinished);
        GameEvents.P2Wins.AddListener(GameFinished);
    }


    void Update()
    {
        if (gameFinished)
            return;

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
        animator.SetBool("Walking", true);

        walking = true;
        bool walkingLeft = moveToPosition.x < transform.position.x;
        avatarRenderer.flipX = !walkingLeft;

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
        animator.SetBool("Walking", false);

        walking = false;

        yield return new WaitForSeconds(duration);

        animationShouldCange = true;
        yield return null;
    }


    void GameFinished()
    {
        gameFinished = true;

        // Make the avatar move to the center of its screen
        moveToPosition = new Vector3(
            (minX + maxX) / 2,
            transform.position.y,
            0
        );
        StartCoroutine(WalkTowards());
    }
}
