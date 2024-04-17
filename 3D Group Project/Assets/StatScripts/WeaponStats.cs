using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Range(1, 6)] public int WepRarity = 1;
    public float WepDamage = 1;
    public float WepMainStat = 1;
    public float WepStamina = 1;
    public float WepAtkp = 1;
    public int WepReqLvl = 1;
    int LegenaryRarity = 1000;
    int EpicRarity = 500;
    int RareRarity = 250;
    int UncommonRarity = 100;
    int CommonRarity = 25;
    int TrashRarity = 5;

    [SerializeField] int WepMinDamage = 5;
    [SerializeField] int WepMaxReq = 1;
    [SerializeField] float WepILvl = 1;

    void Update()
    {
        /*
        if(Random.Range(1, LegenaryRarity) == 1)
        {
            WepRarity = 6;
        }
        else if (Random.Range(1, EpicRarity) == 1)
        {
            WepRarity = 5;
        }
        else if (Random.Range(1, RareRarity) == 1)
        {
            WepRarity = 4;
        }
        else if (Random.Range(1, UncommonRarity) == 1)
        {
            WepRarity = 3;
        }
        else if (Random.Range(1, CommonRarity) == 1)
        {
            WepRarity = 2;
        }
        else if (Random.Range(1, TrashRarity) == 1)
        {
            WepRarity = 1;
        }
        
        WepReqLvl = Random.Range(1, WepMaxReq);
        */
        WepILvl = 20 * WepReqLvl + (3 * WepRarity) / 10;
        WepDamage = (WepILvl /30) * WepReqLvl / 2;
        WepMainStat = WepDamage + (WepILvl * 1.25f) / 45f;
        WepStamina = WepMainStat * 1.25f;
        WepAtkp = 2.5f * (WepMainStat);

        if(WepReqLvl == 3)
        {
            WepDamage = WepMinDamage + 3;
        }
        else if (WepDamage <= WepMinDamage && WepReqLvl == 2)
        {
            WepDamage = WepMinDamage + 2;
        }
        else if (WepDamage <= WepMinDamage)
        {
            WepDamage = WepMinDamage;
        }

        if(WepRarity == 1)
        {
            if(WepReqLvl > 5)
            {
                WepDamage = WepDamage / 3;
                WepILvl = WepILvl / 3;
            }
            WepStamina = 0;
            WepMainStat = 0;
            WepAtkp = 0;
        }
        
        if(WepRarity == 2)
        {
            if(WepReqLvl > 10)
            {
                WepDamage = WepDamage / 3 + 3;
                WepILvl = WepILvl / 3;
            }
            WepStamina = 0;
            WepMainStat = 0;
            WepAtkp = 0;
        }

        if(WepRarity == 3)
        {
            //WepReqLvl = Random.Range(5, WepMaxReq);
            if(WepReqLvl > 15)
            {
                WepAtkp = WepAtkp * 0.75f;
                WepDamage = WepDamage * 0.75f;
                WepILvl = WepILvl * 0.75f;
            }    
            WepStamina = WepStamina / 1.5f;
            WepMainStat = WepMainStat / 1.5f;
        }

        if(WepRarity == 4)
        {
            //WepReqLvl = Random.Range(8, WepMaxReq);
        }

        if(WepRarity == 5)
        {
            //WepReqLvl = Random.Range(15, WepMaxReq);
            WepAtkp = WepAtkp * 1.15f;
            WepILvl = WepILvl * 1.15f;
            WepDamage = WepDamage * 1.15f;
            WepMainStat = WepMainStat * 1.1f;
            WepStamina = WepStamina * 1.2f;
        }
        
        if(WepRarity == 6)
        {
            //WepReqLvl = Random.Range(18, WepMaxReq);
            WepAtkp = WepAtkp * 1.35f;
            WepILvl = WepILvl * 1.35f;
            WepDamage = WepDamage * 1.35f;
            WepMainStat = WepMainStat * 1.2f;
            WepStamina = WepStamina * 1.5f;
        }
        WepILvl = (int) WepILvl;
        WepDamage = (int) WepDamage;
        WepMainStat = (int) WepMainStat;
        WepStamina = (int) WepStamina;
        WepAtkp = (int) WepAtkp;
    }
}
