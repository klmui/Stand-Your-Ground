using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
public static bool GameIsPaused = false;
    public GameObject GameOverMenuUI;

    public void Restart() {
        Time.timeScale = 1f;
        GameOverMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<AudioManager>().Stop("bgm-game");
        FindObjectOfType<AudioManager>().Play("bgm-game");
    }

    public void ReturnToTitle() {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Stop("bgm-game");
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().Play("bgm-title");
    }
}
