using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassFunctions : MonoBehaviour
{

    [SerializeField] GameObject player;

    RangerClassStats rangerClass;
    WarriorClassStats warriorClass;
    MageClassStats mageClass;

    public bool isRanger = false;
    int rdmg;
    int rhp;

    public bool isWarrior = false;
    int wdmg;
    int whp;

    public bool isMage = false;
    int mdmg;
    int mhp;
    int mhealing;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        if(isRanger == true)
        {
            RangerStats();
            rangerClass = player.GetComponent<RangerClassStats>();
        }
        
        if(isWarrior == true)
        {
            WarriorStats();
            warriorClass = player.GetComponent<WarriorClassStats>();
        }
        
        if(isMage == true)
        {
            MageStats();
            mageClass = player.GetComponent<MageClassStats>();
        }
    }

    void Update()
    {
        if(isRanger == true)
        {
            RangerStats();
        }
       
        if(isWarrior == true)
        {
            WarriorStats();
        }

        if(isMage == true)
        {
            MageStats();
        }
    }

    void RangerStats()
    {
        rdmg = (int)rangerClass.RangerDamage;
        rhp = (int)rangerClass.RangerHP;
    }

    void WarriorStats()
    {
        wdmg = (int)warriorClass.WarriorDamage;
        whp = (int)warriorClass.WarriorHP;
    }

    void MageStats()
    {
        mdmg = (int)mageClass.MageDamage;
        mhp = (int)mageClass.MageDamage;
        mhealing = (int)mageClass.MageHealing;
    }
}
