using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls behavior for Dragon SFX Audio
public class DragonAudioControl : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip roar1;
    [SerializeField] private AudioClip heavyImpact;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
