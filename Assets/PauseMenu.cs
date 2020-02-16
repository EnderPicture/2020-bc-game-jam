using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUi;
    // Update is called once per frame

    void Start () {
        pauseMenuUi.SetActive(false);
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
    }

    void Pause() {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void LoadScene()
    {
        GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
}
