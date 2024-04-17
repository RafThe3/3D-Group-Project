using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image crosshair;

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
        UpdateUI();

        //test
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }
    }

    private void UpdateUI()
    {
        healthBar.value = currentHealth;
        healthText.text = $"Health: {currentHealth} / {maxHealth}";

        Ray cameraDirection = new(Camera.main.transform.position, Camera.main.transform.forward);
        crosshair.color = Physics.Raycast(cameraDirection, out RaycastHit hit) && hit.collider.CompareTag("Enemy") ? Color.red
                        : hit.collider.CompareTag("Player") ? Color.blue
                        : Color.white;
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

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
}
