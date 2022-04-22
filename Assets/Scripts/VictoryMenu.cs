using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public GameObject VictoryMenuUI;

    public void PlayNextLevel() {
        Time.timeScale = 1f;
        VictoryMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
