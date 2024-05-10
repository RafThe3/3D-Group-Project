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
    
    //Internal Variables
    private float spawnTimer;
    private int objectsSpawned;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPos = player.transform.position - transform.position;

        bool isReadyToSpawn = spawnTimer >= spawnInterval && ((objectsSpawned < numberOfObjects && !endlessSpawn) || endlessSpawn);
        bool isPlayerNear = playerPos.magnitude < spawnDistance && distanceLimit;
        if (canSpawnObjects && isReadyToSpawn && (isPlayerNear || !distanceLimit))
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
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
}
