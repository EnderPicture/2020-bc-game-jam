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

    public Animator animator;

    public float attackCoolDown;
    float attackLastTime;


    public float followerRadius;

    float maxMove;
    float accMove;
    float dampMove;

    public int health;
    public SpriteRenderer sprite;

    public Transform player;

    public static int ENEMY = 0;
    public static int FOLLOWER = 1;
    public static int BOSS = 2;
    public int mode = ENEMY;

    public GameObject bullet;
    public float coolDown;
    float lastShot;

    public float boostValue;
    public Vector2 boostTimingRange;
    float nextBoost;
    float lastDash;

    void Start()
    {
        maxMove = maxMoveEnemy;
        accMove = accMoveEnemy;
        dampMove = dampMoveEnemy;

        maxMove = maxMove > 0 ? maxMove : 1;
        accMove = accMove > 0 ? accMove : 1;
        generateBossBoost();
    }

    void Update()
    {
        if (mode == FOLLOWER)
        {
            Vector2 mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Vector3.SignedAngle(mousePosition, Vector3.up, Vector3.back);

            float time = Time.realtimeSinceStartup;
            if (Input.GetMouseButton(0) && time > lastShot + coolDown)
            {
                lastShot = time;
                Quaternion bulletSpread = Quaternion.Euler(new Vector3(0, 0, angle));
                GameObject newBullet = GameObject.Instantiate(bullet, transform.position, bulletSpread);
            }

        }
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

        float angle = Vector3.SignedAngle(input, Vector3.up, Vector3.back);
        angle += 180;
        if (angle < 22.5f || angle > 360 - 22.5f)
        {
            animator.Play("IdleEnemy4");
            sprite.flipX = true;
        }
        if (22.5f < angle && angle < (22.5 + 45))
        {
            animator.Play("IdleEnemy3");
            sprite.flipX = true;
        }
        if (22.5f + 45 * 1 < angle && angle < (22.5 + 45) + 45 * 1)
        {
            animator.Play("IdleEnemy2");
            sprite.flipX = true;
        }
        if (22.5f + 45 * 2 < angle && angle < (22.5 + 45) + 45 * 2)
        {
            animator.Play("IdleEnemy1");
            sprite.flipX = true;
        }
        if (22.5f + 45 * 3 < angle && angle < (22.5 + 45) + 45 * 3)
        {
            animator.Play("IdleEnemy0");
            sprite.flipX = true;
        }
        if (22.5f + 45 * 4 < angle && angle < (22.5 + 45) + 45 * 4)
        {
            animator.Play("IdleEnemy1");
            sprite.flipX = false;
        }
        if (22.5f + 45 * 5 < angle && angle < (22.5 + 45) + 45 * 5)
        {
            animator.Play("IdleEnemy2");
            sprite.flipX = false;
        }
        if (22.5f + 45 * 6 < angle && angle < (22.5 + 45) + 45 * 6)
        {
            animator.Play("IdleEnemy3");
            sprite.flipX = false;
        }

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

        if (mode == BOSS && Time.realtimeSinceStartup > lastDash + nextBoost)
        {
            lastDash = Time.realtimeSinceStartup;
            generateBossBoost();
            Debug.Log("boost");
            rb.AddForce(input.x * boostValue, input.y * boostValue, 0, ForceMode.Impulse);
        }

        rb.velocity = velocity;
    }

    void generateBossBoost() {
        
        nextBoost = Random.Range(boostTimingRange.x, boostTimingRange.y);
    }

    public void convertToFollowers()
    {
        mode = FOLLOWER;
        gameObject.layer = LayerMask.NameToLayer("Followers");
        sprite.color = new Color(1, 0, 0);
    }

    public void hit()
    {
        health -= 1;
        if (health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public int getMode()
    {
        return mode;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            other.gameObject.GetComponent<BulletController>().die();
            hit();
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (mode == ENEMY || mode == BOSS)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Followers"))
            {
                EnemyController collidedEnemy = other.gameObject.GetComponent<EnemyController>();
                float time = Time.realtimeSinceStartup;
                if (time > attackLastTime + attackCoolDown)
                {
                    attackLastTime = time;
                    collidedEnemy.hit();
                }
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                float time = Time.realtimeSinceStartup;
                if (time > attackLastTime + attackCoolDown)
                {
                    attackLastTime = time;
                    player.hit();
                }
            }
        }
    }
}
