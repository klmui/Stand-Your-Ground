using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    [SerializeField] private GameManager gameManager;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }    
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        gameManager.SetSwordsActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        gameManager.SetSwordsActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ReturnToTitle() {
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().Stop("bgm-game");
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().Play("bgm-title");
    }
}
