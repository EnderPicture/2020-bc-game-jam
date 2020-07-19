using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Mob enemy that swarms the player and are fairly weak
    - Dead bodies can be used by player to make a strong cannonball-like projectile

*/
public class OnionController : EnemyController
{
    public GameObject FOLLOWER;
    void Start()
    {
        name = "Onion";
        maxMove = 5.8f;
        accMove = 2.5f;
        dampMove = 10;
    }

    protected override void FixedUpdate()
    {
        if(!isDead)
        {
            Vector2 goal = player.position - transform.position;
            float angle = Vector3.SignedAngle(goal.normalized, Vector3.up, Vector3.back);
            angle += 180; // angle 0 is pointing down
            onionAnimate(angle);
            moveTowardsGoalVector(goal);
        } else
        {
            
        }
    }

    void onionAnimate(float angle)
    {

        if (angle < 22.5f || angle > 360 - 22.5f)
        {
            animator.Play("walkEnemyR4");
            sprite.flipX = true;
        }
        else if (22.5f < angle && angle < (22.5 + 45))
        {
            animator.Play("walkEnemyR3");
            sprite.flipX = false;
        }
        else if (22.5f + 45 * 1 < angle && angle < (22.5 + 45) + 45 * 1)
        {
            animator.Play("walkEnemyR2");
            sprite.flipX = true;
        }
        else if (22.5f + 45 * 2 < angle && angle < (22.5 + 45) + 45 * 2)
        {
            animator.Play("walkEnemyR1");
            sprite.flipX = true;
        }
        else if (22.5f + 45 * 3 < angle && angle < (22.5 + 45) + 45 * 3)
        {
            animator.Play("walkEnemyR0");
            sprite.flipX = true;
        }
        else if (22.5f + 45 * 4 < angle && angle < (22.5 + 45) + 45 * 4)
        {
            animator.Play("walkEnemyR1");
            sprite.flipX = false;
        }
        else if (22.5f + 45 * 5 < angle && angle < (22.5 + 45) + 45 * 5)
        {
            animator.Play("walkEnemyR2");
            sprite.flipX = false;
        }
        else if (22.5f + 45 * 6 < angle && angle < (22.5 + 45) + 45 * 6)
        {
            animator.Play("walkEnemyR3");
            sprite.flipX = true;
        }
    }
    public void convertToFollower()
    {
        GameObject follower = Instantiate(FOLLOWER);
        FollowerController followerController = follower.GetComponent<FollowerController>();
        follower.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        followerController.player = player;
        GameObject.Destroy(this.gameObject);
        // gameObject.layer = LayerMask.NameToLayer("Followers");
        // sprite.color = new Color(1, 0, 0);
    }
}
