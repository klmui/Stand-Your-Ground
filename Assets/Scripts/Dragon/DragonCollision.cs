using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollision : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip hit;

    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            source.clip = hit;
            source.Play();
        }
    }

    void onTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            print("STAY");
        }
    }

    void onTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            print("EXIT");
        }
    }
}
