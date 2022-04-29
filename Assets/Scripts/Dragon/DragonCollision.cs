using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollision : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip hit;

    public static HealthSystem DragonHP;

    // Start is called before the first frame update
    void Start()
    {
        DragonHP = new HealthSystem(10);
    }

    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            source.clip = hit;
            source.Play();

            DragonHP.Damage(1);
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
