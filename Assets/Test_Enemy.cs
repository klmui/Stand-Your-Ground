using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSys;

    [SerializeField] private Animator anim;

    [SerializeField] private int hp = 3;
    bool invincible = false;
    float invincibleDuration = 20f / 60f;
    float invincibleEndTime;
    bool dead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (dead)
            return;

        if (invincible)
        {
            if (Time.time >= invincibleEndTime)
                invincible = false;
            else
                return;
        }

        Debug.Log("hit!");

        particleSys.Play();
        hp -= 1;

        if(hp<=0)
        {
            Die();
            return;
        }

        anim.SetTrigger("Hit");
        invincibleEndTime = Time.time + invincibleDuration;
        invincible = true;
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        dead = true;
    }
}
