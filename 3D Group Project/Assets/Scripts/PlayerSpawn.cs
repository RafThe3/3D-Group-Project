using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject mage, ranger, warrior;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        Time.timeScale = 1;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        switch (PlayerPrefs.GetInt("PlayerToSpawn"))
        {
            case 1:
                Instantiate(mage, spawnPoint.position, Quaternion.identity);
                break;

            case 2:
                Instantiate(ranger, spawnPoint.position, Quaternion.identity);
                break;

            case 3:
                Instantiate(warrior, spawnPoint.position, Quaternion.identity);
                break;

            default:
                break;
        }
    }
}
