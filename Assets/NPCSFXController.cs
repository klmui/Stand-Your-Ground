using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSFXController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private AudioClip startAttackSound;
    [SerializeField] private AudioClip gotHitSound;
    [SerializeField] private AudioClip dieSound;


    // Start is called before the first frame update
    void Start()
    {
        PlaySpawnSound();
    }

    public void PlaySpawnSound()
    {
        audioSource.clip = spawnSound;
        audioSource.Play();
    }

    public void PlayStartAttackSound()
    {
        audioSource.clip = startAttackSound;
        audioSource.Play();
    }

    public void PlayGotHitSound()
    {
        audioSource.clip = gotHitSound;
        audioSource.Play();
    }

    public void PlayDieSound()
    {
        audioSource.clip = dieSound;
        audioSource.Play();
    }
}
