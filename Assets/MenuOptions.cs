using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public string GAME_SCENE = "alvinbalance";
    public AudioManager audioManager;
    void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void LoadScene()
    {
        audioManager.playButton();
        SceneManager.LoadScene(GAME_SCENE);
    }
}
