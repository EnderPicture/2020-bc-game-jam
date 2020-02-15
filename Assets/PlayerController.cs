using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector2 lastInput;

    public float maxMove;
    public float accMove;
    public float dampMove;

    public Transform arm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        maxMove = maxMove > 0 ? maxMove : 1;
        accMove = accMove > 0 ? accMove : 1;
    }
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Vector3.SignedAngle(mousePosition, Vector3.up, Vector3.back);
        arm.eulerAngles = new Vector3(0,0,angle);
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

        if (input.magnitude > 0) {
            deltaVelocity *= accMove;
            rb.AddForce(deltaVelocity.x, deltaVelocity.y, 0);
        } else {
            deltaVelocity *= dampMove;
            rb.AddForce(deltaVelocity.x, deltaVelocity.y, 0);
        }

        rb.velocity = velocity;
        lastInput = input;
    }

}
