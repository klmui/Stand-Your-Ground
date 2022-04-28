using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Dragon").GetComponent<DragonCollision>().DragonHP;
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.GetHealthMax();
        healthBar.value = playerHealth.GetHealthMax();
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
}