using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClassFunctions : MonoBehaviour
{
    RangerClassStats rangerClass;
    WarriorClassStats warriorClass;
    MageClassStats mageClass;
    public float finalDamage;
    public float finalHealth;
    public float finalHealing;

    public bool isVoidborn = false;
    public bool isKOE = false;

    public bool isRanger = false;
    float rdmg;
    float rhp;

    public bool isWarrior = false;
    float wdmg;
    float whp;

    public bool isMage = false;
    float mdmg;
    float mhp;
    float mhealing;

    private void Awake()
    {
        if (TryGetComponent(out mageClass))
        {
            mageClass = GetComponent<MageClassStats>();
        }
        else if (TryGetComponent(out rangerClass))
        {
            rangerClass = GetComponent<RangerClassStats>();
        }
        else if (TryGetComponent(out warriorClass))
        {
            warriorClass = GetComponent<WarriorClassStats>();
        }

        if(rangerClass != null)
        {
            isRanger = true;
        }
        else if(warriorClass != null)
        {
            isWarrior = true;
        }
        else if(mageClass != null)
        {
            isMage = true;
        }

        if(isRanger == true)
        {
            RangerStats();
            finalDamage = rdmg;
            finalHealth = rhp;
        }
        
        if(isWarrior == true)
        {
            WarriorStats();
            finalDamage = wdmg;
            finalHealth = whp;
        }
        
        if(isMage == true)
        {
            MageStats();
            finalDamage = mdmg;
            finalHealth = mhp;
            finalHealing = mhealing;
        }
    }

    void Update()
    {
        if(isRanger == true)
        {
            RangerStats();
            finalDamage = rdmg;
            finalHealth = rhp;
        }
       
        if(isWarrior == true)
        {
            WarriorStats();
            finalDamage = wdmg;
            finalHealth = whp;
        }

        if(isMage == true)
        {
            MageStats();
            finalDamage = mdmg;
            finalHealth = mhp;
            finalHealing = mhealing;
        }
        if(isKOE == true)
        {
            finalDamage = (int)(finalDamage * 1.05f);
            finalHealth = (int)(finalHealth * 1.2f);
            finalHealing = (int)(finalHealing * 1.25f);
        }
        if(isVoidborn == true)
        {
            finalDamage = (int)(finalDamage * 1.2f);
            finalHealth = (int)(finalHealth * 1.05f);
            finalHealing = (int)(finalHealing * 1.25f);
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