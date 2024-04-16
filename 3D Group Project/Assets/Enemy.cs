using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private bool isInvincible = false;
    [Min(0), SerializeField] private float startingHealth = 1;
    [Min(0), SerializeField] private float maxHealth = 100;

    //Internal Variables
    private float currentHealth = 0;
    private Slider healthBar;
    private Canvas enemyUI;

    private void Start()
    {
        currentHealth = startingHealth;
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<Slider>();
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = startingHealth;
        enemyUI = GetComponentInChildren<Canvas>();
        enemyUI.enabled = false;
    }

    private void Update()
    {
        Debug.Log($"Enemy Health: {currentHealth}");
    }

    public void TakeDamage(float damage)
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

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
