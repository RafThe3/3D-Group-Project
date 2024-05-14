using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs, spawnPoints;
    [SerializeField] private bool canSpawnObjects = true;
    [Min(0), SerializeField] private int numberOfObjects = 1;
    [Min(0), SerializeField] private float spawnInterval = 1;
    [SerializeField] private bool distanceLimit = false;
    [Min(0), SerializeField] private float spawnDistance = 1;
    [SerializeField] private bool endlessSpawn = false;
    [SerializeField] private bool isEnemiesAsPrefab = false;
    
    //Internal Variables
    private float spawnTimer;
    private int objectsSpawned;
    private EnemyCounter enemyCounter;

    private void Awake()
    {
        if (isEnemiesAsPrefab)
        {
            enemyCounter = GetComponent<EnemyCounter>();
            enemyCounter.UpdateEnemiesToSpawn(numberOfObjects);
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (canSpawnObjects)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPos = player.transform.position - transform.position;

        bool isReadyToSpawn = spawnTimer >= spawnInterval && ((objectsSpawned < numberOfObjects && !endlessSpawn) || endlessSpawn);
        bool isPlayerNear = playerPos.magnitude < spawnDistance && distanceLimit;

        if (!isReadyToSpawn || (!isPlayerNear && distanceLimit))
        {
            return;
        }

        int spawn = Random.Range(0, spawnPoints.Length), obj = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[obj], spawnPoints[spawn].transform.position, Quaternion.identity);
        objectsSpawned++;
        spawnTimer = 0;
    }

    private void OnDrawGizmos()
    {
        if (distanceLimit)
        {
            Gizmos.DrawWireSphere(transform.position, spawnDistance);
        }
    }

    public float GetSpawnDistance()
    {
        return spawnDistance;
    }
}
