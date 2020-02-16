using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public float maxMoveEnemy;
    public float accMoveEnemy;
    public float dampMoveEnemy;
    public float maxMoveFollowers;
    public float accMoveFollowers;
    public float dampMoveFollowers;
    public float maxMoveFollowersInner;
    public float accMoveFollowersInner;
    public float dampMoveFollowersInner;

    public float followerRadius;

    float maxMove;
    float accMove;
    float dampMove;

    public int health;
    public SpriteRenderer sprite;

    public Transform player;

    public static int ENEMY = 0;
    public static int FOLLOWER = 1;
    int mode = ENEMY;

    void Start()
    {
        maxMove = maxMoveEnemy;
        accMove = accMoveEnemy;
        dampMove = dampMoveEnemy;

        maxMove = maxMove > 0 ? maxMove : 1;
        accMove = accMove > 0 ? accMove : 1;
    }
    void FixedUpdate()
    {

        Vector2 input = player.position - transform.position;
        if (mode == FOLLOWER)
        {
            if (input.magnitude < followerRadius)
            {
                input *= -1;
                maxMove = maxMoveFollowersInner;
                accMove = accMoveFollowersInner;
                dampMove = dampMoveFollowersInner;
            }
            else
            {
                maxMove = maxMoveFollowers;
                accMove = accMoveFollowers;
                dampMove = dampMoveFollowers;
            }
        }
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

    public void convertToFollowers()
    {
        mode = FOLLOWER;
        gameObject.layer = LayerMask.NameToLayer("Followers");
        sprite.color = new Color(1, 0, 0);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            other.gameObject.GetComponent<BulletController>().die();
            health -= 1;

            if (health <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
