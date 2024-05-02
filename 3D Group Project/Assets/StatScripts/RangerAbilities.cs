using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAbilities : MonoBehaviour
{
    RangerClassStats rClass;
    ClassFunctions cFunct;
    public bool rAbility1 = false;
    public bool rAbility2 = false;
    public float tDmg = 1;
    float abilCD1 = 10;
    float abilCD2 = 10;
    float tCD1 = 1;
    float tCD2 = 1;
    void Awake()
    {
        rClass = GetComponent<RangerClassStats>();
        cFunct = GetComponent<ClassFunctions>();
        abilCD1 = 20;
        abilCD2 = 8;
    }
    private void FixedUpdate()
    {
        tCD1 += Time.deltaTime;
        tCD2 += Time.deltaTime;

        if(tCD1 >= abilCD1)
        {
            rAbility1 = true;
        }
        
        if(tCD2 >= abilCD2)
        {
            rAbility2 = true;
        }
    }
    //when 1 is pressed set the next attack to be an AoE with 67% of original damage
    public void ExplosiveShot()
    {
        if(rAbility1 == true)
        {
            tDmg = (int)(cFunct.finalDamage * 0.67f);
        }
    }

    // when 2 is pressed set the next attack to be a regular shot with 50% increased damage
    public void AimedShot()
    {
        if (rAbility2 == true)
        { 
            tDmg = (int)(cFunct.finalDamage * 1.5f); 
        }
    }
}
