using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassFunctions : MonoBehaviour
{

    [SerializeField] GameObject player;

    RangerClassStats rangerClass;

    bool isRanger = false;
    int rdmg;
    int rhp;

    bool isWarrior = false;
    int wdmg;
    int whp;

    bool isMage = false;
    int mdmg;
    int mhp;

    private void Awake()
    {
        rangerClass = player.GetComponent<RangerClassStats>();

        if(isRanger == true)
        {
            RangerStats();
        }
    }

    void Update()
    {
        RangerStats();
    }

    void RangerStats()
    {
        rdmg = (int)rangerClass.RangerDamage;
        rhp = (int)rangerClass.RangerHP;
    }



}
