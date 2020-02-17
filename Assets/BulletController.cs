using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody rb;
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
        spawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {

        spriteRenderer.GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
        
        if (mode == 0) {
            speedMod = 1;
            if (spawnTime + health < Time.realtimeSinceStartup)
            {
                die();
            }
        }
        else if (mode == 1)
        {
            speedMod = ((spawnTime + health) - Time.realtimeSinceStartup) / health;
            if (spawnTime + health < Time.realtimeSinceStartup)
            {
                die();
            }
        }
    }
    public void die()
    {
        GameObject.Destroy(this.gameObject);
    }
    void FixedUpdate()
    {
        Vector3 position = transform.localPosition;
        position += transform.up * speed * Time.deltaTime * Mathf.Min((speedMod + .3f), 1);
        transform.localPosition = position;
    }
}
