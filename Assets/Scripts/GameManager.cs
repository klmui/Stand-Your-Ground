using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public float victoryDelay = 1f;
    public GameObject GameOverMenuUI;
    public GameObject VictoryMenuUI;

    [SerializeField] private GameObject[] swords;

    // Update is called once per frame
    void Update() {
        // Autowin
        if (Input.GetKeyDown("w")) {
            if (!gameHasEnded) {
                Victory();
            }
        }    
    }

    public void EndGame() {
        if (!gameHasEnded) {
            gameHasEnded = true;
            FindObjectOfType<AudioManager>().Stop("bgm-game");
            SetSwordsActive(false);
            Invoke("ShowGameOverUI", restartDelay);
        }
    }

    void ShowGameOverUI() {
        FindObjectOfType<AudioManager>().Play("game-over");
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Victory() {
        if (!gameHasEnded) {
            gameHasEnded = true;
            FindObjectOfType<AudioManager>().Stop("bgm-game");
            SetSwordsActive(false);
            Invoke("ShowVictoryUI", victoryDelay);
        }
    }

    void ShowVictoryUI() {
        FindObjectOfType<AudioManager>().Play("victory");
        VictoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetSwordsActive(bool swordActive)
    {
        foreach(GameObject sword in swords)
        {
            sword.SetActive(swordActive);
        }
    }
}
