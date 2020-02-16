using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
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

    Vector3 originalPosition;
    Vector3 newPosition;

    void FixedUpdate()
    {
        originalPosition = transform.localPosition;
        if (shakeDuration > 0)
        {
            newPosition = transform.position + Random.insideUnitSphere * shakeAmount;
            newPosition = new Vector3(newPosition.x, newPosition.y, originalPosition.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * shakeSpeed);
            shakeDuration -= Time.deltaTime;
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
