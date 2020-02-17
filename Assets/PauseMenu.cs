using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public AudioManager audioManager;

    public GameObject pauseMenuUi;
    // Update is called once per frame

    void Start () {
        pauseMenuUi.SetActive(false);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.playGameMusic();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            if (GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        } 
    }

    public void Resume() 
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        audioManager.playButton();
        audioManager.playMusic();
    }

    void Pause() {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        audioManager.playButton();
        audioManager.pauseMusic();
    }

    public void LoadScene()
    {
        GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("alvinbalance");
    }
}
