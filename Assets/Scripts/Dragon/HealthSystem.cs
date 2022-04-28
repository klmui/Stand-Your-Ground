using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private float health;
    private float healthMax;
    public HealthBar healthBar;

    public HealthSystem(int healthMax)
    {
        this.health = healthMax;
        this.healthMax = healthMax;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void UpdateHPBar()
    {
        this.healthBar.SetHealth(GetHealthPercent());
    }

    public float GetHealth()
    {
        return this.health;
    }

    public float GetHealthMax()
    {
        return this.healthMax;
    }

    public void Damage(int damageAmount)
    {
        this.health -= damageAmount;
        if (this.health < 0) this.health = 0;
        UpdateHPBar();
    }

    public void Heal(int healAmount)
    {
        this.health += healAmount;
        if (this.health > this.healthMax) this.health = this.healthMax;
        UpdateHPBar();
    }


}
