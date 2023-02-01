using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 13;
    [SerializeField] float dashSpeed = 40;
    [SerializeField] float dashDuration = .3f;

    bool dashing = false;
    bool stunned = false;
    Coroutine dashCoroutine = null;

    bool dashRequested = false, moveRequested = false;
    Vector3 moveDirection = Vector3.zero;

    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (dashRequested)
        {
            Dash();
        }
        else if (!stunned && !dashing && moveRequested)
        {
            Walk();
        }
    }


    void Walk()
    {
        rb.velocity = moveDirection * walkingSpeed;

        /*
        framesSinceMovedRight = 0;

        if (isGrounded && framesSinceMovedLeft < changeDirDelayFrames)
        {
            changeDirParticles.Play();
        }*/
    }


    void Dash()
    {

        if (dashCoroutine == null)
            dashCoroutine = StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        //int a = 0;
        float t = 0;
        dashing = true;
        dashRequested = false;

        while (t < dashDuration)
        {
            rb.velocity = moveDirection * dashSpeed;

            t += Time.deltaTime;
            //print(a++);

            yield return null;
        }

        rb.velocity = moveDirection * walkingSpeed;

        dashing = false;
        dashCoroutine = null;

        yield return null;
    }


    public void DashRequested()
    {
        // Prevent dash request from being remembered
        // if the character is still dashing
        if (dashCoroutine == null)
            dashRequested = true;
    }


    public Vector3 MoveDirection
    {
        set
        {
            if (value.magnitude > Mathf.Epsilon)
            {
                moveDirection = value;
                moveRequested = true;
            }
            else
            {
                moveRequested = false;
            }
        }
    }
}
