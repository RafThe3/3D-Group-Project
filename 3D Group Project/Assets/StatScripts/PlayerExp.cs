using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExp : MonoBehaviour
{
    public int CurrentExp = 0;
    public int CurrentLevel = 1;
    private int MaxLevel = 20;
    public int MaxExp = 15;

    public Slider ExpBar;
    public TextMeshProUGUI ExpText;
    public TextMeshProUGUI LevelText;
    [SerializeField] private AudioClip levelUpSFX;

    Enemy eHP;
    EnemyStats eStats;

    private void Awake()
    {
        MaxExp = CurrentLevel * 15;
        ExpBar.maxValue = MaxExp;
        //eHP = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        //eStats = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>();
    }

    private void Start()
    {
        SaveData data = new();
        CurrentLevel = data.currentLevel;
        MaxExp = data.maxExp;
        CurrentExp = data.currentExp;
    }

    void Update()
    {
        LevelUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("expOrb"))
        {
            //CurrentExp += eStats.expDropped;
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
            Camera.main.GetComponent<AudioSource>().PlayOneShot(levelUpSFX, 3f);
        }
    }
}
