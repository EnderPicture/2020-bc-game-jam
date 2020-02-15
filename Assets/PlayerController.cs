using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector2 lastDirection;

    public float boostX;
    public float accX;
    public float maxSpeedX;
    public float dampX;

    public float boostJump;
    public float accJump;
    public float accJumpDuration;

    float attackTimer;

    float repairTimer;

    float lastAttacked;

    float lastJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastJump = Time.realtimeSinceStartup;
        lastAttacked = Time.realtimeSinceStartup;
    }
    void Update()
    {
        // Attack Timing
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        if (repairTimer > 0)
            repairTimer -= Time.deltaTime;
    } // Update 
    void FixedUpdate()
    {
    // Movement 
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
    
        Vector2 direction = new Vector2(0, 0);
        Vector3 velocity = rb.velocity;

        if (horizontal > 0) {
            direction.x = 1;
        } else if (horizontal < 0) {
            direction.x = -1;
        }

        if (vertical > 0)
            direction.y = 1;
        else if (vertical < 0)
            direction.y = -1;

        direction = direction.normalized;

    // Movement Feeling
        if (direction.x != lastDirection.x && direction.x != 0) {
            velocity.x = boostX * direction.x;
        } else if (direction.x != 0) {
            rb.AddForce(accX * direction.x * Mathf.Clamp(maxSpeedX - (velocity.x * direction.x), 0, maxSpeedX), 0, 0);
        } else {
            rb.AddForce(-velocity.x * dampX, 0, 0);
        }

        if (direction.y != lastDirection.y && direction.y != 0) {
            velocity.y = boostX * direction.y;
        } else if (direction.y != 0) {
            rb.AddForce(0, accX * direction.y * Mathf.Clamp(maxSpeedX - (velocity.y * direction.y), 0, maxSpeedX), 0);
        } else {
            rb.AddForce(0, -velocity.y * dampX, 0);
        }


        rb.velocity = velocity;
        lastDirection = direction;
    } // FixedUpdate

}
