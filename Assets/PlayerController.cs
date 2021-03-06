﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float maxMove;
    public float accMove;
    public float dampMove;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public InfluencerBubbleController influencer;

    public Transform arm;
    private float savedAngle = 0;

    public float health;

    Vector2 input;

    void Start()
    {
        maxMove = maxMove > 0 ? maxMove : 1;
        accMove = accMove > 0 ? accMove : 1;
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Vector3.SignedAngle(mousePosition, Vector3.up, Vector3.back);
        arm.eulerAngles = new Vector3(0, 0, angle);
        angle += 180;
        savedAngle = angle;

        spriteRenderer.GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);

        if (input.magnitude > 0)
        {
            if (angle < 10.5f || angle > 360 - 10.5f)
            {
                // down
                animator.Play("Walk4");
                spriteRenderer.flipX = true;
            }
            if (10.5f < angle && angle < (22.5 + 10.5f + 45))
            {
                // down right
                animator.Play("Walk3");
                spriteRenderer.flipX = false;
            }
            if (22.5f + 10.5f + 45 * 1 < angle && angle < (22.5 + 10.5f + 45) + 45 * 1)
            {
                // right
                animator.Play("Walk2");
                spriteRenderer.flipX = true;
            }
            if (22.5f + 10.5f + 45 * 2 < angle && angle < (22.5 + 15.5f + 45) + 45 * 2)
            {
                // up right
                animator.Play("Walk1");
                spriteRenderer.flipX = true;
            }
            if (22.5f + 15.5f + 45 * 3 < angle && angle < (22.5 - 15.5f + 45) + 45 * 3)
            {
                // up
                animator.Play("Walk0");
                spriteRenderer.flipX = true;
            }
            if (22.5f - 15.5f + 45 * 4 < angle && angle < (22.5 - 10.5f + 45) + 45 * 4)
            {
                // up left
                animator.Play("Walk1");
                spriteRenderer.flipX = false;
            }
            if (22.5f - 10.5f + 45 * 5 < angle && angle < (22.5 - 10.5f + 45) + 45 * 5)
            {
                // left
                animator.Play("Walk2");
                spriteRenderer.flipX = false;
            }
            if (22.5f - 10.5f + 45 * 6 < angle && angle < (22.5 + 45 + 10.5f) + 45 * 6)
            {
                // down left
                animator.Play("Walk3");
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            if (angle < 10.5f || angle > 360 - 10.5f)
            {
                // down
                animator.Play("Idle4");
                spriteRenderer.flipX = true;
            }
            if (10.5f < angle && angle < (22.5 + 10.5f + 45))
            {
                // down right
                animator.Play("Idle3");
                spriteRenderer.flipX = false;
            }
            if (22.5f + 10.5f + 45 * 1 < angle && angle < (22.5 + 10.5f + 45) + 45 * 1)
            {
                // right
                animator.Play("Idle2");
                spriteRenderer.flipX = true;
            }
            if (22.5f + 10.5f + 45 * 2 < angle && angle < (22.5 + 15.5f + 45) + 45 * 2)
            {
                // up right
                animator.Play("Idle1");
                spriteRenderer.flipX = true;
            }
            if (22.5f + 15.5f + 45 * 3 < angle && angle < (22.5 - 15.5f + 45) + 45 * 3)
            {
                // up
                animator.Play("Idle0");
                spriteRenderer.flipX = true;
            }
            if (22.5f - 15.5f + 45 * 4 < angle && angle < (22.5 - 10.5f + 45) + 45 * 4)
            {
                // up left
                animator.Play("Idle1");
                spriteRenderer.flipX = false;
            }
            if (22.5f - 10.5f + 45 * 5 < angle && angle < (22.5 - 10.5f + 45) + 45 * 5)
            {
                // left
                animator.Play("Idle2");
                spriteRenderer.flipX = false;
            }
            if (22.5f - 10.5f + 45 * 6 < angle && angle < (22.5 + 45 + 10.5f) + 45 * 6)
            {
                // down left
                animator.Play("Idle3");
                spriteRenderer.flipX = true;
            }

        }



        if (Input.GetKeyDown("space"))
        {
            influencer.influence();
        }
    }

    public void hit()
    {
        Debug.Log("player Hit!");
        health -= 1;
    }
    void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        input = new Vector2(horizontal, vertical);
        Vector3 velocity = rb.velocity;

        input = input.normalized;

        Vector2 targetVelocity = input * maxMove;
        Vector2 deltaVelocity = targetVelocity - new Vector2(velocity.x, velocity.y);

        if (input.magnitude > 0)
        {
            deltaVelocity *= accMove;
            rb.AddForce(deltaVelocity.x, deltaVelocity.y, 0);
        }
        else
        {
            deltaVelocity *= dampMove;
            rb.AddForce(deltaVelocity.x, deltaVelocity.y, 0);
        }

        rb.velocity = velocity;
    }

    public float getAngle()
    {
        return savedAngle;
    }
}
