using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject mage, ranger, warrior;
    [SerializeField] private Transform[] spawnPoint;

    private void Start()
    {
        Time.timeScale = 1;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        int spawn = Random.Range(0, spawnPoint.Length);
        switch (PlayerPrefs.GetInt("PlayerToSpawn"))
        {
            case 1:
                Instantiate(mage, spawnPoint[spawn].position, Quaternion.identity);
                break;

            case 2:
                Instantiate(ranger, spawnPoint[spawn].position, Quaternion.identity);
                break;

            case 3:
                Instantiate(warrior, spawnPoint[spawn].position, Quaternion.identity);
                break;

            default:
                break;
        }
    }
}
