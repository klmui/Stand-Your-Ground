using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public Animator m_Animator;

    public GameObject returnToTitleUI;
    
    public void CreditFinished()
    {
        print("Credit finished");
        m_Animator.SetTrigger("BlankScreen");
    }

    public void ReturnToTitle() {
        SceneManager.LoadScene(0);
    }

    public void BlankScreenFinished()
    {
        returnToTitleUI.SetActive(true);
    }
}
