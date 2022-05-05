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

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dmg)
    {
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

        //Play take damage sound [TODO]
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
