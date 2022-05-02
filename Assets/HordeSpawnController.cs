using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private bool doneSpawning;
    private int numEnemiesLeft;

    [System.Serializable]
    public struct HordeSpawnBurst
    {
        public float spawnTime;
        public Transform pathToFollow;
        public int numEnemies;
    }

    private float nextSpawnTime;

    [SerializeField] private List<HordeSpawnBurst> spawnBursts;

    [SerializeField] private EnemySpawnController spawnController;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        numEnemiesLeft = 0;

        for(int i = 0; i<spawnBursts.Count; i++)
        {
            numEnemiesLeft += spawnBursts[i].numEnemies;
        }

        spawnController.SetNumEnemies(numEnemiesLeft);

        GetNextSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (doneSpawning)
            return;

        if (Time.time >= nextSpawnTime)
        {
            Transform pathToFollow = spawnBursts[0].pathToFollow;
            int numEnemiesToSpawn = spawnBursts[0].numEnemies;
            for (int i = 0; i < numEnemiesToSpawn; i++)
            {
                //Spawn enemy
                NPCController spawnedNPC = Instantiate(enemyPrefab).GetComponent<NPCController>();
                spawnedNPC.SetPath(pathToFollow);
            }

            //Pop current time and path
            spawnBursts.RemoveAt(0);

            GetNextSpawnTime();
        }
    }

    public void GetNextSpawnTime()
    {
        if (spawnBursts.Count > 0)
        {
            nextSpawnTime = startTime + spawnBursts[0].spawnTime;
        }
        else
        {
            doneSpawning = true;
        }
    }
}
