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
        SpecScaling(MageSpellpower);

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
            SpecScaling(MageSpellpower);
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
            SpecScaling(MageSpellpower = baseMageSpellpower + wepStats.WepPwr);
            MageIntellect = (baseMageInt * 1) + wepStats.WepMainStat;
            MageStamina = (baseMageStam * 1) + wepStats.WepStamina;
            MageDamage = (MageSpellpower / 10) + tempDamage;
           ;
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

    void SpecScaling(float atkp)
    {
        if (specs.mageSpec1 == true)
        {
            MageSpellpower = (MageIntellect * 2) + (int)(baseMageSpellpower * 1.5);
            MageHP = 25 + (int)(MageStamina * 1.1f) + (playerExp.CurrentLevel * 15);
        }

        else if (specs.mageSpec2 == true)
        {
            MageSpellpower = (MageIntellect * 1.2f) + (int)(baseMageSpellpower * 1.2f);
            MageHealing = (int)(67%MageSpellpower);
            MageHP = 25 + (int)(200%MageStamina) + (playerExp.CurrentLevel * 25);
        }

        else if (specs.mageSpec1 == false && specs.mageSpec2 == false)
        {
            MageSpellpower = (MageIntellect) + (int)(baseMageSpellpower);
            MageHP = 25 + (int)(MageStamina * 1.5f) + (playerExp.CurrentLevel * 15);
        }
    }
    /*
    MageSpellpower = (MageIntellect / 7) + baseMageSpellpower;
        MageHealing = (int) (MageSpellpower / 20);
        MageHP = 25 + (int) (MageStamina* 1.5f) + (playerExp.CurrentLevel* 15);
    */
}