using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageClassStats : MonoBehaviour
{
    public float MageHP = 75;
    public float MageIntellect = 10;
    public float MageStamina = 10;
    public float MageDamage = 10;
    public float MageHealing = 10;
    public float MageSpellpower = 10;
    float baseMageInt = 10;
    float baseMageStam = 5;
    float baseMageSpellpower = 100;

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
        MageIntellect = baseMageInt;
        MageHealing = (baseMageSpellpower / 11);
        MageSpellpower = baseMageSpellpower;
        MageStamina = baseMageStam;
        MageHP = MageStamina * 5;
        MageDamage = (int)MageDamage;
        MageIntellect = (int)MageIntellect;
        MageStamina = (int)MageStamina;
    }

    void FixedUpdate()
    {
        MageSpellpower = (MageIntellect / 7) + baseMageSpellpower;
        MageHealing = (int)(MageSpellpower / 20);
        MageHP = 25 + (int)(MageStamina * 1.5f) + (playerExp.CurrentLevel * 15);

        tempDamage = (int)wepStats.WepDamage;
        baseMageInt = (int)baseMageInt;
        baseMageStam = (int)baseMageStam;
        baseMageSpellpower = (int)baseMageSpellpower;
        MageSpellpower = (int)MageSpellpower;
        MageIntellect = (int)MageIntellect;
        MageStamina = (int)MageStamina;
        MageHealing = (int)MageHealing;
        MageDamage = (int)MageDamage;

        if (wepEquipped == true)
        {
            WeaponEquipped();
        }
        else if (wepEquipped == false)
        {
            MageIntellect = baseMageInt;
            MageHealing = (int)(MageSpellpower / 20);
            MageStamina = baseMageStam;
            MageDamage = (int)(MageSpellpower / 10);
        }
        MageDamage = (int)MageDamage;
        MageHealing = (int)MageHealing;

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
            MageSpellpower = baseMageSpellpower + wepStats.WepPwr;
            MageIntellect = (baseMageInt * 1) + wepStats.WepMainStat;
            MageStamina = (baseMageStam * 1) + wepStats.WepStamina;
            MageDamage = (MageSpellpower / 10) + tempDamage;
            MageHealing = (MageSpellpower / 20) + (tempDamage / 5);
        }
    }

    void LevelUp()
    {
        level++;
        baseMageInt += 5;
        baseMageStam += 5;
        baseMageSpellpower += 75;
        MageIntellect += playerExp.CurrentLevel;
        MageSpellpower = (MageIntellect / 7) + baseMageSpellpower;
        MageStamina += playerExp.CurrentLevel;
        MageHP = 25 + (int)(MageStamina * 1.5f) + (playerExp.CurrentLevel * 15);
        MageHealing += (MageSpellpower / 20);
        MageDamage += (MageSpellpower / 10);


        baseMageInt = (int)baseMageInt;
        baseMageStam = (int)baseMageStam;
        baseMageSpellpower = (int)baseMageSpellpower;
        MageSpellpower = (int)MageSpellpower;
        MageDamage = (int)MageDamage;
        MageIntellect = (int)MageIntellect;
        MageStamina = (int)MageStamina;
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