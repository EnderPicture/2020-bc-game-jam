using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : EnemyController
{
    const float maxMoveFollowers = 2.5f;
    const float accMoveFollowers = .5f;
    const float dampMoveFollowers = 10;
    const float maxMoveFollowersInner = 2.5f;
    const float accMoveFollowersInner = 10;
    const float dampMoveFollowersInner = 10;

    public float followerShootSpread = 5;
    public float followerHealth = 3;

    public float followerRadius = 3;

    public float coolDown = .3f;
    float lastShot;

    // Update is called once per frame
    protected override void Update()
    {
        if (isDead == false && PauseMenu.GameIsPaused == false)
        {
            Vector2 mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Vector3.SignedAngle(mousePosition, Vector3.up, Vector3.back);

            float time = Time.realtimeSinceStartup;
            if (Input.GetMouseButton(0) && time > lastShot + coolDown)
            {
                lastShot = time;
                Quaternion bulletSpread = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-followerShootSpread, followerShootSpread)));
                Instantiate(bullet, transform.position, bulletSpread);
            }

        }
    }
    // Update for physics stuff
    protected override void FixedUpdate()
    {
        Vector2 goal = player.position - transform.position;
        if (goal.magnitude < followerRadius)
        {
            goal *= -1;
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
        float angle = Vector3.SignedAngle(goal.normalized, Vector3.up, Vector3.back);
        angle += 180; // angle 0 is pointing down
        followerAnimate(angle);
        moveTowardsGoalVector(goal);
    }
    public override void die()
    {
        GameObject.Destroy(this.gameObject, 0);
    }
    void followerAnimate(float angle) // angle 0 = down
    {
        if (angle < 22.5f || angle > 360 - 22.5f)
        {
            animator.Play("walkEnemyB4");
            sprite.flipX = true;
        }
        if (22.5f < angle && angle < (22.5 + 45))
        {
            animator.Play("walkEnemyB3");
            sprite.flipX = false;
        }
        if (22.5f + 45 * 1 < angle && angle < (22.5 + 45) + 45 * 1)
        {
            animator.Play("walkEnemyB2");
            sprite.flipX = true;
        }
        if (22.5f + 45 * 2 < angle && angle < (22.5 + 45) + 45 * 2)
        {
            animator.Play("walkEnemyB1");
            sprite.flipX = true;
        }
        if (22.5f + 45 * 3 < angle && angle < (22.5 + 45) + 45 * 3)
        {
            animator.Play("walkEnemyB0");
            sprite.flipX = true;
        }
        if (22.5f + 45 * 4 < angle && angle < (22.5 + 45) + 45 * 4)
        {
            animator.Play("walkEnemyB1");
            sprite.flipX = false;
        }
        if (22.5f + 45 * 5 < angle && angle < (22.5 + 45) + 45 * 5)
        {
            animator.Play("walkEnemyB2");
            sprite.flipX = false;
        }
        if (22.5f + 45 * 6 < angle && angle < (22.5 + 45) + 45 * 6)
        {
            animator.Play("walkEnemyB3");
            sprite.flipX = true;
        }
    }
    protected override void OnCollisionStay(Collision other)
    {
        // do nothing
    }

}
