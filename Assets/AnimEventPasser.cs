using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventPasser : MonoBehaviour
{
    [SerializeField] private UnityEvent event1;

    public void CallEvent1()
    {
        event1.Invoke();
    }
}
