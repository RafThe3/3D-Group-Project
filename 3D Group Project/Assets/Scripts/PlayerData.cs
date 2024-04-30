using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public float x;
    public float y;
    public float z;
    public float currentHealth;
    public float maxHealth;
    public int currentExp, maxExp, currentLevel;

    public PlayerData(Object player)
    {
        Ranger ranger = Object.FindObjectOfType<Ranger>();
        Mage mage = Object.FindObjectOfType<Mage>();
        Warrior warrior = Object.FindObjectOfType<Warrior>();

        if (player == ranger)
        {
            x = ranger.transform.position.x;
            y = ranger.transform.position.y;
            z = ranger.transform.position.z;
            currentHealth = ranger.GetCurrentHealth();
            maxHealth = ranger.GetMaxHealth();
            currentExp = ranger.GetCurrentExp();
            maxExp = ranger.GetMaxExp();
            currentLevel = ranger.GetCurrentLevel();
        }
        else if (player == mage)
        {
            x = mage.transform.position.x;
            y = mage.transform.position.y;
            z = mage.transform.position.z;
            currentHealth = mage.GetCurrentHealth();
            maxHealth = mage.GetMaxHealth();
            currentExp = mage.GetCurrentExp();
            maxExp = mage.GetMaxExp();
            currentLevel = mage.GetCurrentLevel();
        }
        else if (player == warrior)
        {
            x = warrior.transform.position.x;
            y = warrior.transform.position.y;
            z = warrior.transform.position.z;
            currentHealth = warrior.GetCurrentHealth();
            maxHealth = warrior.GetMaxHealth();
            currentExp = warrior.GetCurrentExp();
            maxExp = warrior.GetMaxExp();
            currentLevel = warrior.GetCurrentLevel();
        }
    }
}
