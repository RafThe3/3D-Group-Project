using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject mage, ranger, warrior;
    [SerializeField] private Transform spawnPoint1, spawnPoint2;

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
                Instantiate(mage, PlayerPrefs.GetInt("Faction") == 2 ? spawnPoint1.position : spawnPoint2.position, Quaternion.identity);
                break;

            case 2:
                Instantiate(ranger, PlayerPrefs.GetInt("Faction") == 2 ? spawnPoint1.position : spawnPoint2.position, Quaternion.identity);
                break;

            case 3:
                Instantiate(warrior, PlayerPrefs.GetInt("Faction") == 2 ? spawnPoint1.position : spawnPoint2.position, Quaternion.identity);
                break;

            default:
                break;
        }
    }
}
