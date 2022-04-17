using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFire : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }

    public void ResetTriggers()
    {
        anim.ResetTrigger("FadeIn");
        anim.ResetTrigger("FadeOut");
    }
}
