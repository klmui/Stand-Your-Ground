using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    [SerializeField] private float startingSpawnTime;

    [SerializeField] private GameObject fireballParent;

    private float spawnTime;
    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time + startingSpawnTime;
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned)
            return;

        if (Time.time >= spawnTime)
        {
            fireballParent.SetActive(true);
            spawned = true;
        }
    }
}
