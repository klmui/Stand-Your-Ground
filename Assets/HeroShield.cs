using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroShield : MonoBehaviour
{
    private bool shieldOpen = false;

    private float stamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float startupCost;
    [SerializeField] private float shieldingCostPerSec;
    [SerializeField] private float staminaRechagePerSec;
    [SerializeField] private float cooldownToRechargeDuration;
    private float timeToStartRecharge;

    [Header("References")]
    [SerializeField] private Animator anim;
    [SerializeField] private Slider shieldSlider;

    private void Start()
    {
        timeToStartRecharge = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Block!");

        collision.collider.enabled = false;
    }

    private void Update()
    {
        if (shieldOpen)
        {
            stamina -= shieldingCostPerSec * Time.deltaTime;
            if (stamina <= 0)
                CloseShield();
        }
        else
        {
            if(Time.time >= timeToStartRecharge)
                stamina += staminaRechagePerSec * Time.deltaTime;

            if (stamina > maxStamina)
                stamina = maxStamina;
        }

        UpdateShieldSlider();
    }

    public void ShieldButtonPressed()
    {
        if (stamina > startupCost)
            OpenShield();
    }

    public void ShieldButtonReleased()
    {
        CloseShield();
    }

    public void OpenShield()
    {
        if (shieldOpen == false)
        {
            shieldOpen = true;
            anim.ResetTrigger("Close");
            anim.SetTrigger("Open");
        }
    }

    public void CloseShield()
    {
        if (shieldOpen)
        {
            shieldOpen = false;
            anim.ResetTrigger("Open");
            anim.SetTrigger("Close");
        }
    }

    public void UpdateShieldSlider()
    {
        shieldSlider.value = Mathf.Max(0, stamina / maxStamina);
    }
}
