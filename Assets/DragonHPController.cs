using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonHPController : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int hp;

    private bool invincible = false;
    [SerializeField] private float invincibleDuration;
    private float nextVulnerableTime;

    [SerializeField] private DragonBehavior dragonController;

    [SerializeField] private Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;

        UpdateHPBar();
    }

    private bool attacking;

    public void SetAttacking(int atk) //This is really dumb but apparently you can't pass bools through anim events so ¯\_(-.-)_/¯
    {
        if (atk == 0)
            attacking = false;
        else
            attacking = true;
    }

    public void TakeDamage(int dmg)
    {
        if (attacking)
            return;

        //Check if invincible
        if (invincible)
        {
            if (Time.time >= nextVulnerableTime)
            {
                invincible = false;
            }
            else
            {
                return;
            }
        }

        //Take damage
        hp -= dmg;

        //Play take damage sound
        PlayerSwordSFX.Instance.HitEnemy();

        //Check if dead
        if(hp <= 0)
        {
            hp = 0;

            UpdateHPBar();

            dragonController.Die();
            return;
        }

        //Not dead yet
        UpdateHPBar();

        //Set to invincible
        invincible = true;
        nextVulnerableTime = Time.time + invincibleDuration;
    }

    public void UpdateHPBar()
    {
        hpSlider.value = (float)hp / maxHp;
    }
}
