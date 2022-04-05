using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;

    private float timePerEnemy = 20f;
    private float nextSpawnTime = -1;

    private void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawnTime)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(NPCPrefab);

        nextSpawnTime = Time.time + timePerEnemy;
    }
}
