using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShield : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Block!");

        collision.collider.enabled = false;
    }
}
