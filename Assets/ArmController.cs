using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public GameObject bullet;
    public float coolDown;
    public Transform bulletsContainer;
    public Transform spawnPoint;
    public Rigidbody rb;
    float lastShot;



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float time = Time.realtimeSinceStartup;
        if (Input.GetMouseButton(0) && time > lastShot + coolDown)
        {
            lastShot = time;

            GameObject newBullet = GameObject.Instantiate(bullet, spawnPoint.position, transform.rotation, bulletsContainer);
            BulletController bulletController = newBullet.GetComponent<BulletController>();
            Vector2 force = Helper.AngleVector((transform.eulerAngles.z - 90) * Mathf.Deg2Rad);

            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
