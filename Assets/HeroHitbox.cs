using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHitbox : MonoBehaviour
{
    [SerializeField] private Hero_Stats heroStats;

    private void OnTriggerEnter(Collider other)
    {
        other.enabled = false;

        EnemyCustomDamage customDamage = other.gameObject.GetComponent<EnemyCustomDamage>();
        if (customDamage != null)
            heroStats.TakeDamage(customDamage.Dmg);
        else
            heroStats.TakeDamage(1);
    }
}
