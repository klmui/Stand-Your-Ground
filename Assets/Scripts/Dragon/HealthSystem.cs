using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int health;
    private int healthMax;

    public HealthSystem(int healthMax)
    {
        this.health = healthMax;
        this.healthMax = healthMax;
    }

    public int GetHealth()
    {
        return this.health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        this.health -= damageAmount;
        if (this.health < 0) this.health = 0;
    }

    public void Heal(int healAmount)
    {
        this.health += healAmount;
        if (this.health > this.healthMax) this.health = this.healthMax;
    }
}
