using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Mage : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private bool isInvincible = false;
    [Min(0), SerializeField] private float startingHealth = 1;
    [Min(0), SerializeField] private float maxHealth = 100;
    [Min(0), SerializeField] private int startingHealthPacks = 1;
    [Min(0), SerializeField] private int maxHealthPacks = 1;
    [Min(0), SerializeField] private float healInterval = 1;
    [Min(0), SerializeField] private float autoHealInterval = 1;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image hurtImage;

    //Internal Variables
    private float currentHealth = 0;
    private int healthPacks = 0;
    private float healTimer = 0, autoHealTimer = 0;
    private float tempHealth = 0;
    private MageClassStats mageClass;

    private void Awake()
    {
        mageClass = GetComponent<MageClassStats>();
    }

    private void Start()
    {
        maxHealth = mageClass.MageHP;
        if (startingHealth > maxHealth)
        {
            startingHealth = maxHealth;
        }
        currentHealth = maxHealth;
        healthPacks = startingHealthPacks;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    private void Update()
    {
        healTimer += Time.deltaTime;
        if (currentHealth < maxHealth)
        {
            autoHealTimer += Time.deltaTime;
        }
        else
        {
            autoHealTimer = 0;
        }
        Debug.Log(autoHealTimer);

        FixBugs();
        UpdateUI();

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
            tempHealth += Time.deltaTime * 1;
            currentHealth = Mathf.RoundToInt(tempHealth);
        }
    }

    private void UpdateUI()
    {
        maxHealth = mageClass.MageHP;
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

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }

        currentHealth -= damage;
        autoHealTimer = 0;

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
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
