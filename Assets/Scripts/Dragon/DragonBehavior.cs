using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehavior : MonoBehaviour
{

    public enum ActionState
    {
        Idle1,  // 00
        Walk,   // 01
        Roar,   // 02
        Bite,   // 03
        Claw,   // 04
        Flame,  // 05
        Idle2,  // 06
        Die,    // 07

    }

    [SerializeField] public bool wait; // 1 = wait, 0 = ready to act
    [SerializeField] public ActionState nextAction;
    [SerializeField] private Animator anim;

    [SerializeField] public float rng;

    // Start is called before the first frame update
    void Start()
    {
        wait = false;
        nextAction = ActionState.Idle1;
    }

    // Update is called once per frame
    void Update()
    {
        if (wait == false)
        {
            rng = Random.value;
            wait = true;
            nextAction = (ActionState)Mathf.Floor(Random.Range(0, 8)); // Random format is [minInclusive, maxExclusive)
            anim.SetInteger("Next Action",(int)nextAction);
            anim.SetFloat("RNG", (float) rng);
            anim.SetBool("Wait", wait);
            
        }
    }
}
