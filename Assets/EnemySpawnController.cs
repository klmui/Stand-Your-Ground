using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [System.Serializable] public struct EnemySpawn
    {
        public float spawnTime;
        public Transform pathToFollow;
    }

    [SerializeField] private EnemySpawn[] enemySpawnPattern;

    private List<float> spawnTimes;
    private List<Transform> paths;

    private bool doneSpawning;

    private float nextSpawnTime;

    private int numEnemiesLeft;

    public static EnemySpawnController Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;

        int numEnemies = enemySpawnPattern.Length;
        if(numEnemies > 0)
            numEnemiesLeft = numEnemies;

        spawnTimes = new List<float>();
        paths = new List<Transform>();

        for (int i = 0; i < numEnemies; i++)
        {
            spawnTimes.Add(enemySpawnPattern[i].spawnTime);
            paths.Add(enemySpawnPattern[i].pathToFollow);
        }

        GetNextSpawnTime();
    }

    public void SetNumEnemies(int newNumEnemies)
    {
        numEnemiesLeft = newNumEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (doneSpawning)
            return;

        if (Time.time > nextSpawnTime)
        {
            //Debug.Log(Time.time + ", " + nextSpawnTime);

            //Spawn enemy
            NPCController spawnedNPC = Instantiate(enemyPrefab).GetComponent<NPCController>();
            spawnedNPC.SetPath(paths[0]);

            //Pop current time and path
            spawnTimes.RemoveAt(0);
            paths.RemoveAt(0);

            //Setup for next enemy
            GetNextSpawnTime();
        }
    }

    public void GetNextSpawnTime()
    {
        if (spawnTimes.Count > 0)
        {
            nextSpawnTime = Time.time + spawnTimes[0];
        }
        else
        {
            doneSpawning = true;
        }
    }

    public void EnemyKilled()
    {
        numEnemiesLeft--;

        if(numEnemiesLeft <= 0)
        {
            FindObjectOfType<GameManager>().Victory();
        }
    }
}