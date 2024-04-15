using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    private int CurrentExp = 0;
    private int CurrentLevel = 1;
    private int MaxLevel = 20;
    int MaxExp;

    [SerializeField] Slider ExpBar;
    [SerializeField] Text Level;

    Enemy eHP;
    EnemyStats eStats;
    void Start()
    {
        eHP = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        eStats = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>();
        int MaxExp = CurrentLevel * 15;
    }

    void Update()
    {
        ExpBar.value = CurrentExp;
        LevelUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("expOrb"))
        {
            CurrentExp += eStats.expDropped;
            Destroy(other.gameObject);
        }
    }

    public void GainExp(int exp)
    {
        CurrentExp += exp;
    }
    void LevelUp()
    {
        if(CurrentExp >= MaxExp && CurrentLevel < MaxLevel)
        {
            CurrentLevel++;
            CurrentExp = 0;
        }
    }
}
