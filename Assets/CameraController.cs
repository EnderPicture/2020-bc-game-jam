using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;
    public Camera camera;
    public float cameraRadius = 4.0f;
    public float cameraSpeed = 8.0f;

    public float shakeAmount;
    public float shakeSpeed;
    public float shakeDuration;
    public float decreaseFactor = 1.0f;

    private float pistolShakeAmount = 0.15f;
    private float pistolShakeSpeed = 80f;
    private float pistolShakeDuration = 0.1f;

    private float shotgunShakeAmount = 0.25f;
    private float shotgunShakeSpeed = 90f;
    private float shotgunShakeDuration = 0.1f;
    
    Vector3 shakeNewPos;
    Vector3 mousePos1, mousePos2, screenMouse, mouseOffset, cameraNewPos;

    void Update()
    {
        mousePos1 = Input.mousePosition;
        screenMouse = camera.ScreenToWorldPoint(new Vector3(mousePos1.x, mousePos1.y, transform.position.z - camera.transform.position.z));
        mouseOffset = screenMouse - playerObject.transform.position;
        mousePos2 = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -transform.position.z));

        if (shakeDuration > 0)
        {
            shakeNewPos = transform.position + Random.insideUnitSphere * shakeAmount;
            shakeNewPos = new Vector3(shakeNewPos.x, shakeNewPos.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, shakeNewPos, Time.deltaTime * shakeSpeed);
            shakeDuration -= Time.deltaTime;
        }

        cameraNewPos = new Vector3((mousePos2.x - playerObject.transform.position.x) / 2.0f + playerObject.transform.position.x,
            (mousePos2.y - playerObject.transform.position.y) / 2.0f + playerObject.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, cameraNewPos, Time.deltaTime * cameraSpeed);

        float Dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
            new Vector2(playerObject.transform.position.x, playerObject.transform.position.y));

        if (Dist > cameraRadius)
        {
            var norm = mouseOffset.normalized;
            cameraNewPos = new Vector3(norm.x * cameraRadius + playerObject.transform.position.x, 
                norm.y * cameraRadius + playerObject.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, cameraNewPos, Time.deltaTime * cameraSpeed);
        }
    }

    public void pistolShake()
    {
        setShakeProperties(pistolShakeAmount, pistolShakeSpeed, pistolShakeDuration);
    }

    public void shotgunShake()
    {
        setShakeProperties(shotgunShakeAmount, shotgunShakeSpeed, shotgunShakeDuration);
    }

    public void setShakeProperties(float shakeAmount, float shakeSpeed, float shakeDuration)
    {
        this.shakeAmount = shakeAmount;
        this.shakeSpeed = shakeSpeed;
        this.shakeDuration = shakeDuration;
    }
}
