using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSys;

    [SerializeField] private Animator anim;

    [SerializeField] private NPCController npcController;

    [SerializeField] private int hp = 3;
    bool invincible = false;
    float invincibleDuration = 10f / 60f;
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

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            //Fireball explosion, 1shot enemies
            OnHit(3);
        }
        else
        {
            //Hit by sword, take 1 damage
            OnHit(1);
        }
    }*/

    public void OnHit(int dmg)
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
        PlayerSwordSFX.Instance.HitEnemy();

        //Play blood splatter animation
        particleSys.Play();

        //Take damage
        hp -= dmg;

        if (hp <= 0)
        {
            Die();

            npcController.GotHit(true);

            return;
        }

        npcController.GotHit(false);

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

        EnemySpawnController.Instance.EnemyKilled();
    }

    private void DisableHitboxes()
    {
        foreach (EnemyPartHitbox hitbox in hitboxParts)
        {
            hitbox.enabled = false;
        }
    }
}
