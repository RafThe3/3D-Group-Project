using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAbilities : MonoBehaviour
{
    RangerClassStats rClass;
    ClassFunctions cFunct;
    public bool rAbility1 = false;
    public bool rAbility2 = false;
    float abilCD1 = 10;
    float abilCD2 = 10;
    float tDmg = 1;
    void Awake()
    {
        rClass = GetComponent<RangerClassStats>();
        cFunct = GetComponent<ClassFunctions>();
    }
    //when 1 is pressed set the next attack to be an AoE with 67% of original damage
    public void ExplosiveShot()
    {
        abilCD1 = 20;
        tDmg = (int)(cFunct.finalDamage * 0.67f);
        rAbility1 = true;
    }

    // when 2 is pressed set the next attack to be a regular shot with 50% increased damage
    public void AimedShot()
    {
        abilCD2 = 8;
        tDmg = (int)(cFunct.finalDamage * 1.5f);
        rAbility2 = true;
    }
}
