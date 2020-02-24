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
    public PlayerController player;
    float lastShot;

    public float pistolSpread = 8.0f;
    public float pistolCooldown = 0.2f;
    public float pistolSpeed = 1.5f;
    public int shotgunBullets = 5;
    public float shotgunSpread;
    public float shotgunStartUpSpeedMax;
    public float shotgunStartUpSpeedMin;
    public float shotgunBulletDuration;
    public float shotgunCooldown;
    public float shotgunKnockBack;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CameraController cameraController;
    public AudioManager audioManager;

    void Start()
    {
        cameraController = GameObject.Find("PlayerCamera").GetComponent<CameraController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void pistol()
    {
        coolDown = pistolCooldown;
        var randomAngle = Random.Range(-pistolSpread, pistolSpread);
        Vector3 convertedAngle = transform.rotation.eulerAngles;
        Vector3 bulletSpread = new Vector3(convertedAngle.x, convertedAngle.y, convertedAngle.z + randomAngle);
        GameObject newBullet = GameObject.Instantiate(bullet, spawnPoint.position, Quaternion.Euler(bulletSpread), bulletsContainer);

        BulletController bulletController = newBullet.GetComponent<BulletController>();
        bulletController.GetComponent<BulletController>().speed =
            bulletController.GetComponent<BulletController>().speed * pistolSpeed;

        bulletController.GetComponent<BulletController>().mode = 0;
        Vector2 force = Helper.AngleVector((transform.eulerAngles.z - 90) * Mathf.Deg2Rad);

        rb.AddForce(force, ForceMode.Impulse);
        cameraController.pistolShake();
        audioManager.playShootPistol();
    }

    void shotgun()
    {
        for (int i = 0; i < shotgunBullets; i++) {
            shotGunBullet();
        }
        coolDown = shotgunCooldown;
        cameraController.shotgunShake();
        audioManager.playShootShotgun();
    }

    void shotGunBullet()
    {
        var randomAngle = Random.Range(-shotgunSpread, shotgunSpread);
        Vector3 convertedAngle = transform.rotation.eulerAngles;
        Vector3 bulletSpread = new Vector3(convertedAngle.x, convertedAngle.y, convertedAngle.z + randomAngle);
        GameObject newBullet = GameObject.Instantiate(bullet, spawnPoint.position, Quaternion.Euler(bulletSpread), bulletsContainer);

        BulletController bulletController = newBullet.GetComponent<BulletController>();

        bulletController.GetComponent<BulletController>().mode = 1;
        bulletController.GetComponent<BulletController>().speed =
            bulletController.GetComponent<BulletController>().speed *
            Random.Range(shotgunStartUpSpeedMin, shotgunStartUpSpeedMax);

        bulletController.GetComponent<BulletController>().health = shotgunBulletDuration;

        Vector2 force = Helper.AngleVector((transform.eulerAngles.z - 90) * Mathf.Deg2Rad) * shotgunKnockBack;

        rb.AddForce(force, ForceMode.Impulse);
    }


    void moveArm(){
        float angle = player.getAngle();

        if (angle < 10.5f || angle > 360 - 10.5f)
        {
            // down
            animator.Play("Armswing4");
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = true;
        }
        if (10.5f < angle && angle < (22.5 + 10.5f + 45))
        {
            // down right
            animator.Play("Armswing3");
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = true;

        }
        if (22.5f + 10.5f + 45 * 1 < angle && angle < (22.5 + 10.5f + 45) + 45 * 1)
        {
            // right
            animator.Play("Armswing2");
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
        }
        if (22.5f + 10.5f + 45 * 2 < angle && angle < (22.5 + 15.5f + 45) + 45 * 2)
        { 
            // up right
            animator.Play("Armswing1");
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = true;
        }
        if (22.5f + 15.5f + 45 * 3 < angle && angle < (22.5 - 15.5f + 45) + 45 * 3)
        {
            // up
            animator.Play("Armswing0");
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
        }
        if (22.5f - 15.5f + 45 * 4 < angle && angle < (22.5 - 10.5f + 45) + 45 * 4)
        {
            // up left
            animator.Play("Armswing1");
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = true;
        }
        if (22.5f - 10.5f + 45 * 5 < angle && angle < (22.5 - 10.5f + 45) + 45 * 5)
        {
            // left
            animator.Play("Armswing2");
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
        if (22.5f - 10.5f + 45 * 6 < angle && angle < (22.5 + 45 + 10.5f) + 45 * 6)
        {
            // down left
            animator.Play("Armswing3");
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.GameIsPaused) {
            float time = Time.realtimeSinceStartup;
            if (Input.GetMouseButton(0) && time > lastShot + coolDown)
            {
                lastShot = time;
                shotgun();
            }
            else if (Input.GetMouseButton(1) && time > lastShot + coolDown)
            {
                lastShot = time;
                pistol();
            }
            moveArm();
        } 
    }
}
