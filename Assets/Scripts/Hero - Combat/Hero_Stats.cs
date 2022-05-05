using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero_Stats : MonoBehaviour
{
    public static Hero_Stats Instance;

    [Header("Stats")]
    [SerializeField] private float maxHp;
    private float hp;
    public float Hp => hp;

    [Header("References")]
    [SerializeField] private Slider heroHpSlider;
    [SerializeField] private AudioSource getHitSFX;

    private bool invincible;
    [SerializeField] private float invincibleDuration;
    private float nextVulnerableTime;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Multiple Hero_Stats found");

        hp = maxHp;
        UpdateHPbar();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        {
            //TakeDamage(0.5f);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (invincible)
        {
            if (Time.time >= nextVulnerableTime)
                invincible = false;
            else //player still invincible, don't process damage
                return;
        }

        hp -= dmg;

        getHitSFX.Play();

        if(hp <= 0)
        {
            hp = 0;
            Die();
        }
        else
        {
            //player still alive, give iFrames
            invincible = true;
            nextVulnerableTime = Time.time + invincibleDuration;
        }

        UpdateHPbar();

        //Make player invincible
        //isInvincible = true;
        //heroSpriteRend.color = invincibleColor;
        //endInvincibleTime = Time.time + invincibleDuration;
    }

    public void UpdateHPbar()
    {
        heroHpSlider.value = (float)hp / maxHp;
    }
        
    private void Die()
    {
        FindObjectOfType<GameManager>().EndGame();
    }
}
