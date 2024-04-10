using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private bool isInvincible = false;
    [Min(0), SerializeField] private float startingHealth = 1;
    [Min(0), SerializeField] private float maxHealth = 100;
    [SerializeField] private Slider healthBar;

    //Internal Variables
    private float currentHealth = 0;

    private void Start()
    {
        if (startingHealth > maxHealth)
        {
            startingHealth = maxHealth;
        }
        currentHealth = startingHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = startingHealth;
    }

    private void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }

        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float health)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += health;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
