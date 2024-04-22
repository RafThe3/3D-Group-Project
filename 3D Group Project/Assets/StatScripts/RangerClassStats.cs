using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerClassStats : MonoBehaviour
{
    public float RangerHP = 75;
    public float RangerAgility = 10;
    public float RangerStamina = 10;
    public float RangerDamage = 10;
    public float RangerAtkp = 10;
    float baseRangerAgi = 10;
    float baseRangerStam = 7.5f;
    float baseRangerAtkp = 100;

    int tempDamage = 10;
    int level = 1;

    public bool wepEquipped = false;
    bool canScale = false;

    private Ranger ranger;
    [SerializeField] WeaponStats wepStats;
    PlayerExp playerExp;
    Specializations specs;

    public delegate void WhenPlayerLvlChangedCallback();
    public event WhenPlayerLvlChangedCallback Callback;
    
    void Awake()
    {
        Callback += WhenPlayerXPChanged;
        //wepStats = GetComponent<WeaponStats>();
        playerExp = GetComponent<PlayerExp>();
        specs = GetComponent<Specializations>();
        ranger = GetComponent<Ranger>();

        tempDamage = (int)wepStats.WepDamage;
        RangerAgility = baseRangerAgi;
        RangerAtkp = baseRangerAtkp;
        RangerStamina = baseRangerStam;
        RangerHP = RangerStamina * 10;
        RangerDamage = (int)RangerDamage;
        RangerAgility = (int)RangerAgility;
        RangerStamina = (int)RangerStamina;
    }

    void FixedUpdate()
    {
        SpecScaling(RangerAtkp);
        tempDamage = (int)wepStats.WepDamage;
        baseRangerAgi = (int)baseRangerAgi;
        baseRangerStam = (int)baseRangerStam;
        baseRangerAtkp = (int)baseRangerAtkp;
        RangerAgility = (int)RangerAgility;
        RangerStamina = (int)RangerStamina;
        RangerAtkp = (int)RangerAtkp;
        RangerDamage = (int)RangerDamage;
        Debug.Log($"Ranger Health: {RangerHP}");

        if(wepEquipped == true)
        {
            WeaponEquipped();
        }
        else if(wepEquipped == false)
        {
            RangerAgility = baseRangerAgi;
            SpecScaling(RangerAtkp);
            RangerStamina = baseRangerStam;
            RangerDamage = (int)(RangerAtkp / 7);
        }
        RangerDamage = (int)RangerDamage;

        if (level >= playerExp.CurrentLevel)
        {
            return;
        }
        else if (level < playerExp.CurrentLevel)
        {
            LevelUp();
        }
    }

    void WhenPlayerXPChanged()
    {
        LevelUp();
    }

    
    void WeaponEquipped()
    {
        if(wepStats.WepReqLvl > playerExp.CurrentLevel)
        {
            return;
        }
        else if(wepStats.WepReqLvl <= playerExp.CurrentLevel)
        {
            SpecScaling(RangerAtkp = baseRangerAtkp + wepStats.WepPwr);
            RangerAgility = (baseRangerAgi * 1) + wepStats.WepMainStat;
            RangerStamina = (baseRangerStam * 1) + wepStats.WepStamina;
            RangerDamage = (RangerAtkp / 7) + tempDamage;
        }
    }

    void LevelUp()
    {
        level++;
        baseRangerAgi += 5;
        baseRangerStam += 5;
        baseRangerAtkp += 50;
        RangerStamina += playerExp.CurrentLevel;
        RangerHP = 50 + (int)(RangerStamina * 3f) + (playerExp.CurrentLevel * 25);
        RangerAgility += playerExp.CurrentLevel;
        RangerAtkp += (RangerAgility / 5);
        ranger.SetCurrentHealth(RangerHP);

        baseRangerAgi = (int)baseRangerAgi;
        baseRangerStam = (int)baseRangerStam;
        baseRangerAtkp = (int)baseRangerAtkp;
        RangerDamage = (int)RangerDamage;
        RangerAgility = (int)RangerAgility;
        RangerStamina = (int)RangerStamina;
    }

    void SpecScaling(float atkp)
    {
        if (specs.rangerSpec1 == true)
        {
            RangerAtkp = (int)(RangerAgility * 3f) + (int)(baseRangerAtkp * 3f);
            RangerHP = 50 + (RangerStamina * 2) + (playerExp.CurrentLevel * 10);
        }

        else if (specs.rangerSpec2 == true)
        {
            RangerAtkp = (RangerAgility * 200f) + (int)(baseRangerAtkp * 20f);
            RangerDamage = (int)(RangerAtkp / 1.5f);
            RangerHP = 1;
        }

        else if (specs.rangerSpec1 == false && specs.rangerSpec2 == false)
        {
            RangerAtkp = RangerAgility + baseRangerAtkp;
            RangerHP = 25 + (int)(RangerStamina * 3f) + (playerExp.CurrentLevel * 25);
        }
    }

    /*
    void HealthScale()
    {
        RangerHP = RangerStamina * 5 + (playerExp.CurrentLevel * 75);
    }

    void StamScale()
    {
        RangerStamina += playerExp.CurrentLevel;
    }

    void AgiScale()
    {
        RangerAgility += playerExp.CurrentLevel;
        RangerAtkp = baseRangerAtkp + (RangerAgility / 3);
    }
    */
}
