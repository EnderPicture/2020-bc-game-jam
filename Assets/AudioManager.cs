using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playShootPistol() {
        audioSource.PlayOneShot(audioClips[0]);
    }

    public void playShootShotgun() {
        audioSource.PlayOneShot(audioClips[1]);
    }
}
