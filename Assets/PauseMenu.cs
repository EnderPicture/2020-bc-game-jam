using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    GameObject[] pauseObjects;

    public GameObject pauseMenuUi;
    // Update is called once per frame

    void Start () {
        pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
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
        foreach(GameObject p in pauseObjects) {
            p.SetActive(true);
        }
    }

    void Pause() {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        foreach(GameObject p in pauseObjects) {
            p.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
