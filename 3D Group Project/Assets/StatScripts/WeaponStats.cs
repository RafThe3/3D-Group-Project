using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Range(1, 5)] public int WepRarity = 1;
    public float WepDamage = 1;
    public float WepMainStat = 1;
    public float WepStamina = 1;
    public int WepReqLvl = 1;
    [SerializeField] int WepMinDamage = 5;
    [SerializeField] int WepMaxReq = 1;
    [SerializeField] float WepILvl = 1;

    void Awake()
    {
        WepReqLvl = Random.Range(1, WepMaxReq);
        WepILvl = 20 * WepReqLvl + (3 * WepRarity) / 10;
        WepDamage = WepILvl * WepReqLvl / 30f;
        WepMainStat = WepDamage + (WepILvl * 2.5f) / 12.5f;
        WepStamina = WepMainStat * 1.15f;
        if(WepDamage <= WepMinDamage)
        {
            WepDamage = WepMinDamage;
        }
        if (WepDamage <= WepMinDamage && WepReqLvl == 2)
        {
            WepDamage = WepMinDamage + 2;
        }
        if (WepDamage <= WepMinDamage && WepReqLvl == 3)
        {
            WepDamage = WepMinDamage + 2;
        }
        WepILvl = (int) WepILvl;
        WepDamage = (int) WepDamage;
        WepMainStat = (int) WepMainStat;
        WepStamina = (int) WepStamina;
    }
}
