using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        FindObjectOfType<AudioManager>().Stop("bgm-title");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<AudioManager>().Play("bgm-game");
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PlayHordeLevel(int levelNum)
    {
        FindObjectOfType<AudioManager>().Stop("bgm-title");
        SceneManager.LoadScene(levelNum);
        FindObjectOfType<AudioManager>().Play("bgm-game");
    }

    public void PlayDragonLevel()
    {

    }
}
