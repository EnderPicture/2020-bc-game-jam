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

    public void playGameMusic() {
        audioSource.PlayOneShot(audioClips[17]);
    }

    public void playZombie() {
        int audioClipNumber = Random.Range(4, 5);
        audioSource.PlayOneShot(audioClips[audioClipNumber]);
    }

    public void playMainMenu() {
        audioSource.PlayOneShot(audioClips[8]);
    }

    public void playButton() {
        int audioClipNumber = Random.Range(13, 16);
        audioSource.PlayOneShot(audioClips[audioClipNumber]);
    }

    public void pauseMusic() {
        audioSource.Pause();
    }

    public void playMusic() {
        audioSource.Play();
    }
}
