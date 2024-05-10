using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private string areaToConquer;

    private int enemiesToSpawn, enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEnemiesToSpawn(int enemies)
    {
        enemiesToSpawn = enemies;
    }
}
