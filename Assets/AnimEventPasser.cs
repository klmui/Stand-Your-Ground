using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventPasser : MonoBehaviour
{
    [SerializeField] private UnityEvent event1;
    [SerializeField] private UnityEvent event2;
    [SerializeField] private UnityEvent event3;
    [SerializeField] private UnityEvent event4;
    [SerializeField] private UnityEvent event5;
    [SerializeField] private UnityEvent event6;

    [SerializeField] private NPCController controller;

    public void CallEvent1()
    {
        //Debug.Log("Event1");
        event1.Invoke();
    }

    public void CallEvent2()
    {
        //Debug.Log("Event2");
        event2.Invoke();
    }

    public void CallEvent3()
    {
        //Debug.Log("Event3");
        event3.Invoke();
    }

    public void CallEvent4()
    {
        //Debug.Log("Event4");
        event4.Invoke();
    }

    public void CallEvent5()
    {
        //Debug.Log("Event5");
        event5.Invoke();
    }

    public void PlayStepSFX()
    {
        //Debug.Log("Event5");
        event6.Invoke();
    }

    public void EnableSword()
    {
        controller.EnableSword();
    }

    public void DisableSword()
    {
        controller.DisableSword();
    }
}
