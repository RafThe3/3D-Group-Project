using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExp : MonoBehaviour
{
    private int CurrentExp = 0;
    public int CurrentLevel = 1;
    private int MaxLevel = 20;
    private int MaxExp = 15;

    [SerializeField] Slider ExpBar;
    [SerializeField] TextMeshProUGUI Level;

    Enemy eHP;
    EnemyStats eStats;
    void Start()
    {
        eHP = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        eStats = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>();
        MaxExp = CurrentLevel * 15;
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

    public void LevelUp()
    {
        if(CurrentExp >= MaxExp && CurrentLevel < MaxLevel)
        {
            CurrentLevel++;
            CurrentExp = 0;
        }
    }
}
