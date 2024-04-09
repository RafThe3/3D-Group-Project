using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool isInvincible = false;
    [Min(0), SerializeField] private int startingHealth = 1;
    [Min(0), SerializeField] private int maxHealth = 100;

    private int currentHealth = 0;
    private Slider healthBar;
    private Canvas enemyUI;

    private void Start()
    {
        currentHealth = startingHealth;
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = startingHealth;
        enemyUI = GetComponentInChildren<Canvas>();
        enemyUI.enabled = false;
    }

    private void Update()
    {
        FixBugs();

        Debug.Log($"Enemy Health: {currentHealth}");
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }

        if (currentHealth == healthBar.maxValue)
        {
            enemyUI.enabled = true;
        }

        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixBugs()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
