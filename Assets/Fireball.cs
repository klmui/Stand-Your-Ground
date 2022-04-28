using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit trigger: " + other.gameObject.name);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit collider: " + collision.gameObject.name);
    }
}
