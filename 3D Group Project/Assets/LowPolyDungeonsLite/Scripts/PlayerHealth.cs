using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private bool isInvincible = false;
    [Min(0), SerializeField] private int startingHealth = 1;
    [Min(0), SerializeField] private int maxHealth = 1;
    [SerializeField] private Slider healthBar;

    private int currentHealth = 0;

    private void Start()
    {
        currentHealth = startingHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = startingHealth;
    }

    private void Update()
    {
        FixBugs();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }

        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
