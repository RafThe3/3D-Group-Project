using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClassStats : MonoBehaviour
{
    public float WarriorHP = 75;
    public float WarriorStrength = 10;
    public float WarriorStamina = 10;
    public float WarriorDamage = 10;
    public float WarriorAttackpower = 10;
    float baseWarriorStr = 10;
    float baseWarriorStam = 10;
    float baseWarriorAtkp = 100;

    int tempDamage = 10;
    int level = 1;

    public bool wepEquipped = false;
    bool ws1 = false;
    bool ws2 = false;
    bool canScale = false;

    //[SerializeField] GameObject player;
    [SerializeField] WeaponStats wepStats;
    PlayerExp playerExp;
    Specializations specs;

    public delegate void WhenPlayerLvlChangedCallback();
    public event WhenPlayerLvlChangedCallback Callback;

    void Awake()
    {
        Callback += WhenPlayerXPChanged;
        wepStats = GetComponent<WeaponStats>();
        playerExp = GetComponent<PlayerExp>();
        specs = GetComponent<Specializations>();


        tempDamage = (int)wepStats.WepDamage;
        WarriorStrength = baseWarriorStr;
        WarriorAttackpower = baseWarriorAtkp;
        WarriorStamina = baseWarriorStam;
        WarriorHP = WarriorStamina * 10;
        WarriorDamage = (int)WarriorDamage;
        WarriorStrength = (int)WarriorStrength;
        WarriorStamina = (int)WarriorStamina;
    }

    void FixedUpdate()
    {
        SpecScaling(WarriorAttackpower);
        tempDamage = (int)wepStats.WepDamage;
        baseWarriorStr = (int)baseWarriorStr;
        baseWarriorStam = (int)baseWarriorStam;
        baseWarriorAtkp = (int)baseWarriorAtkp;
        WarriorStrength = (int)WarriorStrength;
        WarriorStamina = (int)WarriorStamina;
        WarriorAttackpower = (int)WarriorAttackpower;
        WarriorDamage = (int)WarriorDamage;

        if(wepEquipped == true)
        {
            WeaponEquipped();
        }
        else if(wepEquipped == false)
        {
            WarriorStrength = baseWarriorStr;
            WarriorStamina = baseWarriorStam;
            SpecScaling(WarriorAttackpower);
            WarriorDamage = (int)(WarriorAttackpower / 10);
        }
        WarriorDamage = (int)WarriorDamage;
        WarriorAttackpower = (int)WarriorAttackpower;

        if(level >= playerExp.CurrentLevel)
        {
            return;
        }
        else if(level < playerExp.CurrentLevel)
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
            SpecScaling(WarriorAttackpower = baseWarriorAtkp + wepStats.WepPwr);
            //WarriorAttackpower = baseWarriorAtkp + wepStats.WepPwr;
            WarriorStrength = (baseWarriorStr * 1) + wepStats.WepMainStat;
            WarriorStamina = (baseWarriorStam * 1) + wepStats.WepStamina;
            WarriorDamage = (WarriorAttackpower / 10) + tempDamage;
        }
    }

    void LevelUp()
    {
        level++;
        baseWarriorStr += 15;
        baseWarriorStam += 10;
        baseWarriorAtkp += 40;
        WarriorStamina += playerExp.CurrentLevel;
        WarriorHP = 50 + (WarriorStamina * 3) + (playerExp.CurrentLevel * 20);
        WarriorStrength += playerExp.CurrentLevel;
        WarriorAttackpower += (int)WarriorStrength + baseWarriorAtkp;


        baseWarriorStr = (int)baseWarriorStr;
        baseWarriorStam = (int)baseWarriorStam;
        baseWarriorAtkp = (int)baseWarriorAtkp;
        WarriorDamage = (int)WarriorDamage;
        WarriorStrength = (int)WarriorStrength;
        WarriorStamina = (int)WarriorStamina;
    }

    void SpecScaling(float atkp)
    {
        if (specs.warriorSpec1 == true)
        {
            WarriorAttackpower = (int)(WarriorStrength * 2f) + (int)(baseWarriorAtkp * 2f);
            WarriorHP = 50 + (WarriorStamina * 2) + (playerExp.CurrentLevel * 10);
        }

        else if (specs.warriorSpec2 == true)
        {
            WarriorAttackpower = (WarriorStrength / 5f) + (int)(baseWarriorAtkp);
            WarriorHP = 50 + (WarriorStamina * 5) + (playerExp.CurrentLevel * 25);
        }

        else if (specs.warriorSpec1 == false && specs.warriorSpec2 == false)
        {
            WarriorAttackpower = WarriorStrength + baseWarriorAtkp;
            WarriorHP = 50 + (WarriorStamina * 3) + (playerExp.CurrentLevel * 20);
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