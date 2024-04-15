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
    int level = 1;

    //[SerializeField] GameObject player;
    WeaponStats wepStats;
    PlayerExp playerExp;
    
    void Awake()
    {
        wepStats = GetComponent<WeaponStats>();
        playerExp = GetComponent<PlayerExp>();
       
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



        if(level <= playerExp.CurrentLevel)
        {
            return;
        }
            level++;
            OnLevelUp();
        
        /*
        WeaponEquipped();
        */
    }


    void WeaponEquipped()
    {

        if(wepStats.WepReqLvl > playerExp.CurrentLevel)
        {
            return;
        }
        else if(wepStats.WepReqLvl <= playerExp.CurrentLevel)
        {
            RangerAgility += wepStats.WepMainStat;
            RangerStamina += wepStats.WepStamina;
            RangerDamage = RangerDamage + wepStats.WepDamage + RangerAtkp;
        }
    }

    void OnLevelUp()
    {
        RangerStamina += playerExp.CurrentLevel;
        RangerHP += RangerStamina * 5 + (playerExp.CurrentLevel * 75);
        RangerAgility += playerExp.CurrentLevel;
        RangerAtkp += (RangerAgility / 3);
        baseRangerAgi += 5;
        baseRangerStam += 5;
        baseRangerAtkp += 100;

        baseRangerAgi = (int)baseRangerAgi;
        baseRangerStam = (int)baseRangerStam;
        baseRangerAtkp = (int)baseRangerAtkp;
        RangerDamage = (int)RangerDamage;
        RangerAgility = (int)RangerAgility;
        RangerStamina = (int)RangerStamina;
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
