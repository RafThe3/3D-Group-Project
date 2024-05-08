using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int enemyMaxLevel = 23;
    public int enemyLevelRange = 23;
    public float enemyDamage = 1;
    public int expDropped = 1;
    [SerializeField] GameObject expOrb1;
    [SerializeField] int zoneLevelMin = 1; //this will need to be somewhere else
    [SerializeField] int enemyCurrentLevel = 1;

    PlayerExp dropExp;

    void Start()
    {
        dropExp = GameObject.FindWithTag("Player").GetComponent<PlayerExp>();
    }

    void Awake()
    {
        enemyCurrentLevel = Random.Range(zoneLevelMin, enemyLevelRange);
        if(enemyCurrentLevel > enemyMaxLevel)
        {
            enemyCurrentLevel = enemyMaxLevel;
        }
        enemyDamage = enemyCurrentLevel * 20 / 3;
        enemyDamage = (int)enemyDamage;
        expDropped = enemyCurrentLevel * 3;
        Debug.Log(enemyDamage);
        Debug.Log(expDropped);
    }

    
    void Update()
    {
        enemyDamage = enemyCurrentLevel * 20 / 3;
        enemyDamage = (int)enemyDamage;
        expDropped = enemyCurrentLevel * 3;
        Debug.Log(enemyDamage);
        Debug.Log(expDropped);
    }

    void OnDestroy()
    {

        //Instantiate<GameObject>(expOrb1, transform.position, Quaternion.identity);

        dropExp.GainExp(expDropped);
    }
}
