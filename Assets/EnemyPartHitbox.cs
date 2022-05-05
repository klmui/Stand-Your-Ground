using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartHitbox : MonoBehaviour
{
    [SerializeField] private EnemyStats enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            //Fireball explosion, 1shot enemies
            enemy.OnHit(3);
        }
        else
        {
            //Hit by sword, take 1 damage
            enemy.OnHit(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            //Fireball explosion, 1shot enemies
            enemy.OnHit(3);
        }
        else
        {
            //Hit by sword, take 1 damage
            enemy.OnHit(1);
        }
    }

    private void Start()
    {
        enemy.AddHitboxPart(this);
    }
}
