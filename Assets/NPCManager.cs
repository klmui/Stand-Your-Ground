using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    [SerializeField] private int maxEnemiesThatCanAttackAtOnce;
    [SerializeField] private Transform playerTransform;

    public Transform PlayerTransform => playerTransform;

    private int numCurrAttackingEnemies;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Multiple NPCManagers found");

        numCurrAttackingEnemies = 0;
    }

    public void EnemyDoneAttack()
    {
        numCurrAttackingEnemies--;
    }

    public bool CanNewEnemyAttack()
    {
        if(numCurrAttackingEnemies < maxEnemiesThatCanAttackAtOnce)
        {
            numCurrAttackingEnemies++;
            return true;
        }

        return false;
    }
}
