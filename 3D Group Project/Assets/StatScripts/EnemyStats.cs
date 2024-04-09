using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int enemyMaxLevel = 1;
    public int enemyLevelRange = 23;
    public float enemyDamage = 1;
    [SerializeField] int zoneLevelMin = 1; //this will need to be somewhere else
    [SerializeField] int enemyCurrentLevel = 1;




    void Awake()
    {
        enemyCurrentLevel = Random.Range(zoneLevelMin, 23);
        if(enemyCurrentLevel > enemyMaxLevel)
        {
            enemyCurrentLevel = enemyMaxLevel;
        }
        enemyDamage = enemyCurrentLevel * 20 / 3;
    }

    
    void Update()
    {
        
    }
}
