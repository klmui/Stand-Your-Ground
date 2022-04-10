using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSys;

    [SerializeField] private Animator anim;

    [SerializeField] private int hp = 3;
    bool invincible = false;
    float invincibleDuration = 20f / 60f;
    float invincibleEndTime;
    bool dead = false;

    private List<EnemyPartHitbox> hitboxParts;

    private void Start()
    {
        hitboxParts = new List<EnemyPartHitbox>();
    }

    public void AddHitboxPart(EnemyPartHitbox hitbox)
    {
        hitboxParts.Add(hitbox);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHit();
    }

    public void OnHit()
    {
        //Don't process getting hit if already dead
        if (dead)
            return;

        //If the enemy is invincible, don't take damage
        if (invincible)
        {
            if (Time.time >= invincibleEndTime)
                invincible = false;
            else
                return;
        }


        //Show debug message
        Debug.Log("hit!");

        //Play blood splatter animation
        particleSys.Play();

        //Take damage
        hp -= 1;

        if (hp <= 0)
        {
            Die();
            return;
        }

        //If code hits here, enemy has taken damage but has not been killed

        //Set hit trigger, which transitions enemy's animation to the "take damage" animation
        anim.SetTrigger("Hit");

        //Set enemy to invincible
        invincibleEndTime = Time.time + invincibleDuration;
        invincible = true;
    }

    private void Die()
    {
        //Play dying animation and set dead bool to true
        anim.SetTrigger("Die");

        GetComponent<NavMeshAgent>().enabled = false;
        DisableHitboxes();

        dead = true;
    }

    private void DisableHitboxes()
    {
        foreach (EnemyPartHitbox hitbox in hitboxParts)
        {
            hitbox.enabled = false;
        }
    }
}
