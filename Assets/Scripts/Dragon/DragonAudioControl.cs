using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls behavior for Dragon SFX Audio
public class DragonAudioControl : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip roar1;
    [SerializeField] private AudioClip heavyImpact;
    [SerializeField] private AudioClip bite;
    [SerializeField] private AudioClip flamethrower;
    [SerializeField] private AudioClip death;


    public void playRoar1()
    {
        source.clip = roar1;
        source.Play();
    }

    public void playStomp()
    {
        source.clip = heavyImpact;
        source.Play();
    }

    public void playBite()
    {
        source.clip = bite;
        source.Play();
    }

    public void playFlamethrower()
    {
        source.clip = flamethrower;
        source.Play();
    }

    public void playDeath()
    {
        source.clip = death;
        source.Play();
    }

}
