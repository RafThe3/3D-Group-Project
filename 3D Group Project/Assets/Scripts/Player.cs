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
    [Min(0), SerializeField] private int startingHealthPacks = 1;
    [Min(0), SerializeField] private int maxHealthPacks = 1;
    [Min(0), SerializeField] private float healthInterval = 1;
    [SerializeField] private Slider healthBar;

    //Internal Variables
    private float currentHealth = 0;
    private int healthPacks = 0;
    private float healTimer = 0;

    private void Start()
    {
        if (startingHealth > maxHealth)
        {
            startingHealth = maxHealth;
        }
        currentHealth = startingHealth;
        healthPacks = startingHealthPacks;
        healthBar.maxValue = maxHealth;
        healthBar.value = startingHealth;
    }

    private void Update()
    {
        healTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && healthPacks > 0)
        {
            Heal(10);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }

        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }

        currentHealth -= damage;

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
        if (currentHealth < maxHealth && healTimer >= healthInterval)
        {
            currentHealth += health;
            healthPacks--;
            healTimer = 0;
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

    public void AddHealthPack()
    {
        if (healthPacks < maxHealthPacks)
        {
            healthPacks++;
        }
    }
}
