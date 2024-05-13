using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorStats : MonoBehaviour
{
    [Range(1, 6)] public int ArmorRarity = 1;

    public float ArmorMainStat = 1;
    public float ArmorStamina = 1;
    public float ArmorPwr = 1;
    public int ArmorReqLvl = 1;

    [HideInInspector] public int LegenaryRarity = 1000;
    [HideInInspector] public int EpicRarity = 500;
    [HideInInspector] public int RareRarity = 250;
    [HideInInspector] public int UncommonRarity = 100;
    [HideInInspector] public int CommonRarity = 25;
    [HideInInspector] public int TrashRarity = 5;
    [SerializeField] bool isBoots = false;
    [SerializeField] bool isLegs = false;
    [SerializeField] bool isHelm = false;
    [SerializeField] bool isChest = false;
    [SerializeField] int ArmorMaxReq = 1;
    [SerializeField] float ArmorILvl = 1;

    void Awake()
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

        if (CompareTag("Frying Pan"))
        {
            return;
        }

        ArmorILvl = 20 * ArmorReqLvl + (3 * ArmorRarity) / 10;
        ArmorMainStat = (ArmorILvl * 1.25f) / 25f;
        ArmorStamina = ArmorMainStat * 1.25f;
        ArmorPwr = 2.5f * (ArmorMainStat);

        if (ArmorRarity == 1)
        {
            if (ArmorReqLvl > 5)
            {
                ArmorILvl = ArmorILvl / 3;
            }
            ArmorStamina = 0;
            ArmorMainStat = 0;
            ArmorPwr = 0;
        }

        if (ArmorRarity == 2)
        {
            if (ArmorReqLvl > 10)
            {
                ArmorILvl = ArmorILvl / 3;
            }
            ArmorStamina = 0;
            ArmorMainStat = 0;
            ArmorPwr = 0;
        }

        if (ArmorRarity == 3)
        {
            ArmorReqLvl = Random.Range(5, ArmorMaxReq);
            if (ArmorReqLvl > 15)
            {
                ArmorPwr = ArmorPwr * 0.75f;
                ArmorILvl = ArmorILvl * 0.75f;
            }
            ArmorStamina = ArmorStamina / 1.5f;
            ArmorMainStat = ArmorMainStat / 1.5f;
        }

        if (ArmorRarity == 4)
        {
            ArmorReqLvl = Random.Range(8, ArmorMaxReq);
        }

        if (ArmorRarity == 5)
        {
            ArmorReqLvl = Random.Range(15, ArmorMaxReq);
            ArmorPwr = ArmorPwr * 1.15f;
            ArmorILvl = ArmorILvl * 1.15f;
            ArmorMainStat = ArmorMainStat * 1.1f;
            ArmorStamina = ArmorStamina * 1.2f;
        }

        if (ArmorRarity == 6)
        {
            ArmorReqLvl = Random.Range(18, ArmorMaxReq);
            ArmorPwr = ArmorPwr * 1.35f;
            ArmorILvl = ArmorILvl * 1.35f;

            ArmorMainStat = ArmorMainStat * 1.2f;
            ArmorStamina = ArmorStamina * 1.5f;
        }

        if(isHelm == true)
        {
            ArmorPwr = ArmorPwr * 0.90f;
            ArmorMainStat = ArmorMainStat * 0.95f;
            ArmorStamina = ArmorStamina * 1.05f;
        }
        if(isChest == true)
        {
            ArmorPwr = ArmorPwr * 1.25f;
            ArmorMainStat = ArmorMainStat * 1.35f;
            ArmorStamina = ArmorStamina * 1.5f;
        }
        if(isLegs == true)
        {
            ArmorPwr = ArmorPwr * 1.05f;
            ArmorMainStat = ArmorMainStat * 1.1f;
            ArmorStamina = ArmorStamina * 1.35f;
        }
        if(isBoots == true)
        {
            ArmorPwr = ArmorPwr * 0.90f;
            ArmorMainStat = ArmorMainStat * 1f;
            ArmorStamina = ArmorStamina * 1.15f;
        }
        ArmorILvl = (int)ArmorILvl;
        ArmorMainStat = (int)ArmorMainStat;
        ArmorStamina = (int)ArmorStamina;
        ArmorPwr = (int)ArmorPwr;
    }
}

