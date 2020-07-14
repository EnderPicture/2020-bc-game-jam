using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{

    public Color flashColor = Color.white;
    public float flashDuration = .1f;

    Material mat;

    private IEnumerator flashCoroutine;

    private void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        mat.SetColor("_FlashColor", flashColor);
    }

    public void Flash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = DoFlash();
        StartCoroutine(flashCoroutine);
    }


    private IEnumerator DoFlash()
    {
        float lerpTime = 0;

        while (lerpTime < flashDuration)
        {
            lerpTime += Time.deltaTime;
            float perc = lerpTime / flashDuration;

            SetFlashAmount(1.3f - perc);
            yield return null;
        }
        SetFlashAmount(0);
    }
    private void SetFlashAmount(float flashAmount)
    {
        mat.SetFloat("_FlashAmount", flashAmount);
    }

}
