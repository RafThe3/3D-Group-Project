using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float WepRarity = 1;
    public float WepDamage = 1;
    public float WepMainStat = 1;
    public float WepStamina = 1;
    public int WepReqLvl = 1;
    [SerializeField] float WepILvl = 1;

    void Awake()
    {
        
        WepReqLvl = Random.Range(1, 20);
        WepILvl = 20 * WepReqLvl + (3 * WepRarity);
        WepDamage = WepILvl * WepReqLvl;
        WepMainStat = WepDamage + (WepILvl * 2.5f);
        WepStamina = WepMainStat;

    }

}
