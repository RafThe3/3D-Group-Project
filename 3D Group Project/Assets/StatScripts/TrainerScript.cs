using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerScript : MonoBehaviour
{
    Specializations specs;
    ClassFunctions stats;
    PlayerExp lvl;
    Inventory inv;
    void Awake()
    {
        specs = GameObject.FindWithTag("Player").GetComponent<Specializations>();
        stats = GameObject.FindWithTag("Player").GetComponent<ClassFunctions>();
        lvl = GameObject.FindWithTag("Player").GetComponent<PlayerExp>();
        inv = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    public void ChooseSpecialization1()
    {
        specs.mageSpec1 = false;
        specs.mageSpec2 = false;
        
        specs.rangerSpec1 = false;
        specs.rangerSpec2 = false;

        specs.warriorSpec1 = false;
        specs.warriorSpec2 = false;
    }
    
    public void ChooseSpecialization2()
    {
        specs.mageSpec1 = true;
        specs.mageSpec2 = false;
        
        specs.rangerSpec1 = true;
        specs.rangerSpec2 = false;

        specs.warriorSpec1 = true;
        specs.warriorSpec2 = false;
    }
    
    public void ChooseSpecialization3()
    {
        specs.mageSpec1 = false;
        specs.mageSpec2 = true;
        
        specs.rangerSpec1 = false;
        specs.rangerSpec2 = true;

        specs.warriorSpec1 = false;
        specs.warriorSpec2 = true;
    }

    public void IncreaseDamage()
    {
        if(inv.Coins != 0)
        {
            inv.Coins--;
            stats.finalDamage += lvl.CurrentLevel;
        }
        else { return; }
    }

    public void IncreaseHealth()
    {
        if(inv.Coins != 0)
        {
            inv.Coins--;
            stats.finalHealth += lvl.CurrentLevel;
        }
        else { return; }
    }

    public void IncreaseHealing()
    {
        if(specs.mageSpec1 == false && inv.Coins != 0)
        {
            inv.Coins--;
            stats.finalHealing += lvl.CurrentLevel;
        }
        else { return; }
    }
}
