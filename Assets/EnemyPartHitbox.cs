using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartHitbox : MonoBehaviour
{
    [SerializeField] private EnemyStats enemy;

    private void OnTriggerEnter(Collider other)
    {
        enemy.OnHit();
    }

    private void Start()
    {
        enemy.AddHitboxPart(this);
    }
}
