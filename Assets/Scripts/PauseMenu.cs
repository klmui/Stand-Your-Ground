using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    [SerializeField] private InputAction pauseControllerButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePressed();
        }
    }

    public void PausePressed()
    {
        //don't allow pausing if game is over
        if (GameManager.Instance.GameHasEnded)
        {
            GameEnded(); //disable swords and menus
            return;
        }

        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        GameManager.Instance.SetSwordsActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameManager.Instance.SetSwordsActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void GameEnded()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        GameManager.Instance.SetSwordsActive(false);
        Time.timeScale = 0f;
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().Stop("bgm-game");
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().Play("bgm-title");
    }
}
