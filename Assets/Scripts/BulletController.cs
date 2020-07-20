using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Player-sided bullets, may need to make this a super class and have children class 
public class BulletController : MonoBehaviour
{
    //Rigidbody rb;
    public float speed;

    public float health;
    float spawnTime;

    float speedMod = 0;

    public SpriteRenderer spriteRenderer;

    //0 for pistol, 1 for shotgun
    public int mode = 0;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        spriteRenderer.GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
        
        if (mode == 0) {
            speedMod = 1;
            if (spawnTime + health < Time.time)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
        else if (mode == 1)
        {
            speedMod = ((spawnTime + health) - Time.time) / health;
            if (spawnTime + health < Time.time)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            Vector2 rotated = Helper.AngleVector(transform.eulerAngles.z * Mathf.Deg2Rad);
            rotated = Quaternion.Euler(0, 0, Random.Range(80, 100)) * rotated;
            if (enemy.isDeadFromHit())
            {
                enemy.rb.AddForce(rotated * 5f, ForceMode.Impulse);
            }
            else
            {
                enemy.rb.AddForce(rotated, ForceMode.Impulse);
            }
            enemy.hit();
            GameObject.Destroy(this.gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    void FixedUpdate()
    {
        if(PauseMenu.GameIsPaused == false) {
            Vector3 position = transform.localPosition;
            position += transform.up * speed * Time.fixedDeltaTime * Mathf.Min((speedMod + .3f), 1);
            transform.localPosition = position;
        }
    }
}
