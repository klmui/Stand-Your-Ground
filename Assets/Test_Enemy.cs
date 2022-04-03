using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem system;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit!");
        system.Play();
    }
}
