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
    float baseWarrriorAtkp = 100;

    int tempDamage = 10;
    int level = 1;

    public bool wepEquipped = false;
    bool canScale = false;

    //[SerializeField] GameObject player;
    WeaponStats wepStats;
    PlayerExp playerExp;

    public delegate void WhenPlayerLvlChangedCallback();
    public event WhenPlayerLvlChangedCallback Callback;

    void Awake()
    {
        Callback += WhenPlayerXPChanged;
        wepStats = GetComponent<WeaponStats>();
        playerExp = GetComponent<PlayerExp>();

        tempDamage = (int)wepStats.WepDamage;
        WarriorStrength = baseWarriorStr;
        WarriorAttackpower = baseWarrriorAtkp;
        WarriorStamina = baseWarriorStam;
        WarriorHP = WarriorStamina * 10;
        WarriorDamage = (int)WarriorDamage;
        WarriorStrength = (int)WarriorStrength;
        WarriorStamina = (int)WarriorStamina;
    }

    void FixedUpdate()
    {
        WarriorAttackpower = (WarriorStrength / 1.5f) + baseWarrriorAtkp;
        WarriorHP = 50 + (WarriorStamina * 3) + (playerExp.CurrentLevel * 20);

        tempDamage = (int)wepStats.WepDamage;
        baseWarriorStr = (int)baseWarriorStr;
        baseWarriorStam = (int)baseWarriorStam;
        baseWarrriorAtkp = (int)baseWarrriorAtkp;
        WarriorStrength = (int)WarriorStrength;
        WarriorStamina = (int)WarriorStamina;
        WarriorAttackpower = (int)WarriorAttackpower;
        WarriorDamage = (int)WarriorDamage;

        if (wepEquipped == true)
        {
            WeaponEquipped();
        }
        else if (wepEquipped == false)
        {
            WarriorStrength = baseWarriorStr;
            WarriorAttackpower = baseWarrriorAtkp;
            WarriorStamina = baseWarriorStam;
            WarriorDamage = (int)(WarriorAttackpower / 10);
        }
        WarriorDamage = (int)WarriorDamage;

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
        if (wepStats.WepReqLvl > playerExp.CurrentLevel)
        {
            return;
        }
        else if (wepStats.WepReqLvl <= playerExp.CurrentLevel)
        {
            WarriorAttackpower = baseWarrriorAtkp + wepStats.WepPwr;
            WarriorStrength = (baseWarriorStr * 1) + wepStats.WepMainStat;
            WarriorStamina = (baseWarriorStam * 1) + wepStats.WepStamina;
            WarriorDamage = (WarriorAttackpower / 7) + tempDamage;
        }
    }

    void LevelUp()
    {
        level++;
        baseWarriorStr += 15;
        baseWarriorStam += 10;
        baseWarrriorAtkp += 40;
        WarriorStamina += playerExp.CurrentLevel;
        WarriorHP = 50 + (WarriorStamina * 3) + (playerExp.CurrentLevel * 20);
        WarriorStrength += playerExp.CurrentLevel;
        WarriorAttackpower += (int)(WarriorStrength / 1.5f) + baseWarrriorAtkp;


        baseWarriorStr = (int)baseWarriorStr;
        baseWarriorStam = (int)baseWarriorStam;
        baseWarrriorAtkp = (int)baseWarrriorAtkp;
        WarriorDamage = (int)WarriorDamage;
        WarriorStrength = (int)WarriorStrength;
        WarriorStamina = (int)WarriorStamina;
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