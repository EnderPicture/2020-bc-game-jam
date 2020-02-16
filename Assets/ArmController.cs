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

    public float pistolSpread = 8.0f;
    public float pistolCooldown = 0.2f;
    public float pistolSpeed = 1.5f;
    public float shotgunSpread;
    public float shotgunStartUpSpeedMax;
    public float shotgunStartUpSpeedMin;
    public float shotgunBulletDuration;
    public float shotgunCooldown;
    public float shotgunKnockBack;
    void Start()
    {
    }

    void pistol() {
        coolDown = pistolCooldown;
        var randomAngle = Random.Range(-pistolSpread, pistolSpread);
        Vector3 convertedAngle = transform.rotation.eulerAngles;
        Vector3 bulletSpread = new Vector3(convertedAngle.x, convertedAngle.y, convertedAngle.z  + randomAngle);
        GameObject newBullet = GameObject.Instantiate(bullet, spawnPoint.position, Quaternion.Euler(bulletSpread), bulletsContainer);

        BulletController bulletController = newBullet.GetComponent<BulletController>();
        bulletController.GetComponent<BulletController>().speed = 
            bulletController.GetComponent<BulletController>().speed * pistolSpeed;

        bulletController.GetComponent<BulletController>().mode = 0;
        Vector2 force = Helper.AngleVector((transform.eulerAngles.z - 90) * Mathf.Deg2Rad);
        
        rb.AddForce(force, ForceMode.Impulse);
    }

    void shotgun() {
        shotGunBullet();
        shotGunBullet();
        shotGunBullet();
        shotGunBullet();
        shotGunBullet();
        coolDown = shotgunCooldown;
    }

    void shotGunBullet() {
        var randomAngle = Random.Range(-shotgunSpread, shotgunSpread);
        Vector3 convertedAngle = transform.rotation.eulerAngles;
        Vector3 bulletSpread = new Vector3(convertedAngle.x, convertedAngle.y, convertedAngle.z  + randomAngle);
        GameObject newBullet = GameObject.Instantiate(bullet, spawnPoint.position, Quaternion.Euler(bulletSpread), bulletsContainer);
        
        BulletController bulletController = newBullet.GetComponent<BulletController>();

        bulletController.GetComponent<BulletController>().mode = 1;
        bulletController.GetComponent<BulletController>().speed = 
            bulletController.GetComponent<BulletController>().speed * 
            Random.Range(shotgunStartUpSpeedMin, shotgunStartUpSpeedMax);
        
        bulletController.GetComponent<BulletController>().health = shotgunBulletDuration;
        Debug.Log(bulletController.GetComponent<BulletController>().health);

        Vector2 force = Helper.AngleVector((transform.eulerAngles.z - 90) * Mathf.Deg2Rad) * shotgunKnockBack;
        
        rb.AddForce(force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.realtimeSinceStartup;
        if (Input.GetMouseButton(0) && time > lastShot + coolDown)
        {   
            lastShot = time;
            shotgun();
        }
        else if (Input.GetMouseButton(1) && time > lastShot + coolDown) {
            lastShot = time;
            pistol();
        }
    }
}
