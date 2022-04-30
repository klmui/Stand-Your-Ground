using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHitbox : MonoBehaviour
{
    [SerializeField] private Hero_Stats heroStats;

    private void OnTriggerEnter(Collider other)
    {
        heroStats.TakeDamage(1);
    }
}
