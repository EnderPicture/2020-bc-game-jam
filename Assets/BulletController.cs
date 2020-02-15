using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    public float health;
    float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime + health < Time.realtimeSinceStartup)
        {
            die();
        }
    }
    public void die()
    {
        GameObject.Destroy(this.gameObject);
    }
    void FixedUpdate()
    {
        Vector3 position = transform.localPosition;
        position += transform.up * speed * Time.deltaTime;
        transform.localPosition = position;
    }
}
