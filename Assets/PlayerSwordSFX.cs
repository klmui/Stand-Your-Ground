using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordSFX : MonoBehaviour
{
    [SerializeField] private AudioSource swordHitSound;

    public static PlayerSwordSFX Instance;

    private void Start()
    {
        Instance = this;
    }

    public void HitEnemy()
    {
        swordHitSound.Play();
    }
}
