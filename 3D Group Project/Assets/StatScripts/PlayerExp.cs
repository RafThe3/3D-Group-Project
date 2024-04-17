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
    int MaxExp = 15;

    [SerializeField] Slider ExpBar;
    [SerializeField] TextMeshProUGUI ExpText;
    [SerializeField] TextMeshProUGUI Level;

    Enemy eHP;
    EnemyStats eStats;

    private void Awake()
    {
        MaxExp = CurrentLevel * 15;
        ExpBar.maxValue = MaxExp;
    }

    void Start()
    {
        eHP = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        eStats = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>();
    }

    void Update()
    {
        ExpBar.value = CurrentExp;
        ExpText.text = $"Exp: {CurrentExp} / {MaxExp}";
        Level.text = $"Level: {CurrentLevel}";
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
            MaxExp = CurrentLevel * 15;
            ExpBar.maxValue = MaxExp;
            CurrentExp = 0;
        }
    }
}
