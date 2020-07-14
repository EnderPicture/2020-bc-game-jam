using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public AudioManager audioManager;

    public GameObject pauseMenuUi;
    // Update is called once per frame

    void Start () {
        pauseMenuUi.SetActive(true);
        GameIsPaused = false;
        Time.timeScale = 1;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.playGameMusic();
    }
    void Update()
    {
        if(!GameIsPaused && pauseMenuUi.activeSelf)
        {
            pauseMenuUi.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            }
            else {
                GameIsPaused = true;
                Pause();
                GameIsPaused = true;
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
        GameIsPaused = true;
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        audioManager.playButton();
        audioManager.pauseMusic();
    }

    public void LoadScene()
    {
        GameIsPaused = false;
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
        GameIsPaused = false;
        pauseMenuUi.SetActive(false);
    }
}
