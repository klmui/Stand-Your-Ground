using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollision : MonoBehaviour
{
    //[SerializeField] private AudioSource source;
    //[SerializeField] private AudioClip getHit;

    [SerializeField] private DragonHPController hpController;

    public void OnTriggerEnter(Collider other)
    {
        //source.clip = getHit;
        //source.Play();

        hpController.TakeDamage(1);
    }
}
