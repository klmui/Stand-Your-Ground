using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private string[] attackTriggerStrings;

    [System.Serializable] public struct AttackStruct
    {
        public string attackName;
        public int[] attackTriggers;
    }

    [SerializeField] private AttackStruct[] attackCombos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Attack1");
            DoAttack(0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Attack2");
            DoAttack(1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Attack3");
            DoAttack(2);
        }
    }

    public void DoAttack(int attackIndex)
    {
        foreach (int attackTrigger in attackCombos[attackIndex].attackTriggers)
        {
            anim.SetTrigger(attackTriggerStrings[attackTrigger]);
        }
    }

    public void DoRandomAttack()
    {
        DoAttack(Random.Range(0, attackCombos.Length));
    }
}
