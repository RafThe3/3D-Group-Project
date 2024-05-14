using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpawner : MonoBehaviour
{
    private void Update()
    {
        Spawner enemySpawner = GetComponent<Spawner>();
        EnemyCounter enemyCounter = GetComponent<EnemyCounter>();
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position - transform.position;
        bool isPlayerNear = playerPos.magnitude < enemySpawner.GetSpawnDistance();

        enemySpawner.enabled = isPlayerNear;
        enemyCounter.enabled = isPlayerNear;
    }
}
