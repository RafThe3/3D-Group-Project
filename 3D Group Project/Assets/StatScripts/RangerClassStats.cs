using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerClassStats : MonoBehaviour
{
    public float RangerHP = 75;
    public float RangerAgility = 10;
    public float RangerStamina = 10;
    public float RangerDamage = 10; //maybe remove this
    public float RangerSpeed = 15; //maybe remove this
    public bool ScaleOn = true;
    bool canEquip = true;
    bool stamCanScale = true;
    bool agiCanScale = true;

    [SerializeField] GameObject player;
    WeaponStats wepStats;
    PlayerExp playerExp;
    
    void Start()
    {
        wepStats = player.GetComponent<WeaponStats>();
        playerExp = player.GetComponent<PlayerExp>();
    }

    void Update()
    {
        if(ScaleOn == true)
        {
            canEquip = true;
            stamCanScale = true;
            agiCanScale = true;
        }
        HealthScale();
        AgiScale();
        StamScale();
        WeaponEquipped();
        RangerDamage = (int)RangerDamage;
        RangerAgility = (int)RangerAgility;
        RangerStamina = (int)RangerStamina;
    }

    public void WeaponEquipped()
    {
        if(wepStats.WepReqLvl > playerExp.CurrentLevel)
        {
            return;
        }
        else if(wepStats.WepReqLvl <= playerExp.CurrentLevel)
        {
            RangerAgility = wepStats.WepMainStat;
            RangerStamina = wepStats.WepStamina;
            RangerDamage = RangerDamage + wepStats.WepDamage + (RangerAgility / 3);
            canEquip = false;
        }
        canEquip = false;
    }

    void HealthScale()
    {
        RangerHP = RangerStamina * 5 + (playerExp.CurrentLevel * 75);
    }

    void StamScale()
    {
        RangerStamina = RangerStamina + (playerExp.CurrentLevel * 7);
        stamCanScale = false;
    }

    void AgiScale()
    {
        RangerAgility = RangerAgility + (playerExp.CurrentLevel * 10);
        agiCanScale = false;
    }
}
