using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    //protected string filename = "none"; //filename convention for animations (----dead, ----idle,  )
    public Rigidbody rb;
    public SpawnEnemyManager se;

    [SerializeField] protected Animator animator;

    public float attackCoolDown;
    float attackLastTime;

    protected float maxMove = 1;
    protected float accMove = 1;
    protected float dampMove = 1;

    public float health;
    [SerializeField] protected SpriteRenderer sprite;

    public Transform player;

    public float deathTime = 10;

    public GameObject bullet; //Prefab with bullet

    public float boostValue = 10;
    public Vector2 boostTimingRange;
    float nextBoost;
    float lastDash = 0;

    protected bool isDead = false;
    protected float deadDrag = 20; //how fast dead bodies stop moving after killed

    protected virtual void Update() //should call it in child class if override
    {
        //dynamically set sorting order to make objects appear behind other objects correctly
        if (!isDead)
        {
            sprite.GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
        } 
        else
        {
            sprite.GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100)-200; // should put into another sorting layer
        }
    }

    protected virtual void FixedUpdate()
    {

    }
    protected virtual void moveTowardsGoalVector(Vector2 goal)
    {
        if (isDead == false)
        {
            Vector3 velocity = rb.velocity;

            goal = goal.normalized;

            Vector2 targetVelocity = goal * maxMove;
            Vector2 deltaVelocity = targetVelocity - new Vector2(velocity.x, velocity.y);

            if (goal.magnitude > 0)
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
    public void setHealth(float health)
    {
        this.health = health;
    }

    void miniCharge(Vector2 direction)
    {
        if (Time.realtimeSinceStartup > lastDash + nextBoost)
        {
            lastDash = Time.realtimeSinceStartup;
            nextBoost = Random.Range(boostTimingRange.x, boostTimingRange.y);
            rb.AddForce(direction.x * boostValue, direction.y * boostValue, 0, ForceMode.Impulse);
        }
    }

    public virtual void hit(int damage = 1)
    {
        health -= damage;
        sprite.GetComponent<SpriteFlash>().Flash();
        if (health <= 0) 
        {
            die();
        }
    }

    public virtual void die()
    {
        se.enemyAliveCount--; // reduce count of global alive enemies
        gameObject.layer = LayerMask.NameToLayer("deadObj");
        Destroy(this.gameObject, deathTime);
        isDead = true;
        animator.Play("dead" + Random.Range(0, 4));
        rb.drag = deadDrag;
    }
    public bool isDeadFromHit(int damage = 1)
    {
        return (health-damage) <= 0;
    }
    protected virtual void OnCollisionStay(Collision other) // collision damage
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
