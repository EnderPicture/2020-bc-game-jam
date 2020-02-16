using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float maxMove;
    public float accMove;
    public float dampMove;

    public Animator animator;

    public Transform arm;

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
        Debug.Log(angle);
        if (angle < 22.5f || angle > 360 - 22.5f) {
            animator.Play("Idle4");
        }
        if ( 22.5f < angle && angle < (22.5+45))
        {
            animator.Play("Idle3");
        }
        if ( 22.5f + 45 * 1 < angle && angle < (22.5+45) + 45 * 1)
        {
            animator.Play("Idle2");
        }
        if ( 22.5f + 45 * 2 < angle && angle < (22.5+45) + 45 * 2)
        {
            animator.Play("Idle1");
        }
        if ( 22.5f + 45 * 3 < angle && angle < (22.5+45) + 45 * 3)
        {
            animator.Play("Idle0");
        }
        if ( 22.5f + 45 * 4 < angle && angle < (22.5+45) + 45 * 4)
        {
            animator.Play("Idle7");
        }
        if ( 22.5f + 45 * 5 < angle && angle < (22.5+45) + 45 * 5)
        {
            animator.Play("Idle6");
        }
        if ( 22.5f + 45 * 6 < angle && angle < (22.5+45) + 45 * 6)
        {
            animator.Play("Idle5");
        }

        if (Input.GetKeyDown("space"))
        {

        }
    }
    void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector2 input = new Vector2(horizontal, vertical);
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
}
