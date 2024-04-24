using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Warrior : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private bool isInvincible = false;
    [Min(0), SerializeField] private float startingHealth = 1;
    [Min(0), SerializeField] private float maxHealth = 100;
    [Min(0), SerializeField] private int startingHealthPacks = 1;
    [Min(0), SerializeField] private int maxHealthPacks = 1;
    [Min(0), SerializeField] private float healInterval = 1;
    [Min(0), SerializeField] private float autoHealInterval = 1;
    [Min(0), SerializeField] private float autoHealMultiplier = 1;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    //[SerializeField] private Image crosshair;

    //Internal Variables
    private float currentHealth = 0;
    private int healthPacks = 0;
    private float tempHealth = 0;
    private float healTimer = 0, autoHealTimer = 0;
    private WarriorClassStats warriorClass;
    private bool canSetTempHealth = true;

    private void Awake()
    {
        warriorClass = GetComponent<WarriorClassStats>();
    }

    private void Start()
    {
        maxHealth = warriorClass.WarriorHP;
        if (startingHealth > maxHealth)
        {
            startingHealth = maxHealth;
        }
        currentHealth = maxHealth;
        healthPacks = startingHealthPacks;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        tempHealth = currentHealth;
    }

    private void Update()
    {
        healTimer += Time.deltaTime;
        autoHealTimer += Time.deltaTime;

        FixBugs();
        UpdateUI();

        if (canSetTempHealth)
        {
            tempHealth = currentHealth;
        }

        //test
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(10);
        }

        if (autoHealTimer >= autoHealInterval && currentHealth < maxHealth)
        {
            AutoHeal();
        }
    }

    private void UpdateUI()
    {
        maxHealth = warriorClass.WarriorHP;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthText.text = $"Health: {currentHealth} / {maxHealth}";

        /*
        Ray cameraDirection = new(Camera.main.transform.position, Camera.main.transform.forward);
        crosshair.color = Physics.Raycast(cameraDirection, out RaycastHit hit) && hit.collider.CompareTag("Enemy") ? Color.red
                        : hit.collider.CompareTag("Player") ? Color.blue
                        : Color.white;
        */
    }

    private void AutoHeal()
    {
        canSetTempHealth = false;
        tempHealth += Time.deltaTime * autoHealMultiplier;
        currentHealth = Mathf.RoundToInt(tempHealth);
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }

        autoHealTimer = 0;
        canSetTempHealth = true;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float health)
    {
        if (currentHealth < maxHealth && healTimer >= healInterval)
        {
            currentHealth += health;
            healthPacks--;
            healTimer = 0;
            autoHealTimer = 0;
            canSetTempHealth = true;
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

    public void SetCurrentHealth(float health)
    {
        currentHealth = health;
    }

    private void FixBugs()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}
