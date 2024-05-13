using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Ranger : MonoBehaviour
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
    [SerializeField] private AudioClip healSFX;

    //Internal Variables
    private float currentHealth = 0;
    private int healthPacks = 0;
    private float healTimer = 0, autoHealTimer = 0;
    private RangerClassStats rangerClass;
    private PlayerExp playerExp;
    private ClassFunctions classes;
    private Animator animator;
    private CharacterController character;

    private int currentLevel = 1, maxExp = 1, currentExp = 1;
    private bool canSetTempHealth = true;
    private float tempHealth = 0;

    private void Awake()
    {
        rangerClass = GetComponent<RangerClassStats>();
        playerExp = GetComponent<PlayerExp>();
        classes = GetComponent<ClassFunctions>();
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
    }

    private void Start()
    {
        currentLevel = playerExp.CurrentLevel;
        maxExp = playerExp.MaxExp;
        currentExp = playerExp.CurrentExp;
        maxHealth = classes.finalHealth;
        if (startingHealth > maxHealth)
        {
            startingHealth = maxHealth;
        }
        currentHealth = classes.finalHealth;
        //currentHealth = maxHealth;
        healthPacks = startingHealthPacks;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        tempHealth = currentHealth;
    }

    private void Update()
    {
        healTimer += Time.deltaTime;
        autoHealTimer += Time.deltaTime;

        FixBugs();
        UpdateUI();

        bool isMoving = Mathf.Abs(character.velocity.z) > Mathf.Epsilon;
        animator.SetBool("isMoving", isMoving);

        if (canSetTempHealth)
        {
            tempHealth = currentHealth;
        }

        if (autoHealTimer >= autoHealInterval && currentHealth < maxHealth)
        {
            AutoHeal();
        }
    }

    private void AutoHeal()
    {
        canSetTempHealth = false;
        tempHealth += Time.deltaTime * autoHealMultiplier;
        currentHealth = Mathf.RoundToInt(tempHealth);
    }

    private void UpdateUI()
    {
        currentLevel = playerExp.CurrentLevel;
        maxExp = playerExp.MaxExp;
        currentExp = playerExp.CurrentExp;

        playerExp.ExpBar.value = currentExp;
        playerExp.ExpBar.maxValue = maxExp;
        playerExp.ExpText.text = $"Exp: {currentExp} / {maxExp}";
        playerExp.LevelText.text = $"Level: {currentLevel}";

        maxHealth = rangerClass.RangerHP;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        /*
        if (levelCheck >= playerExp.CurrentLevel)
        {
            levelCheck = playerExp.CurrentLevel;
        }
        else if(levelCheck < playerExp.CurrentLevel)
        {
            giveHealth = true;
            if (giveHealth)
            {
                currentHealth += (maxHealth*2);
            }

            healthBar.value = healthBar.maxValue;
            
            if(currentHealth >= maxHealth)
            {
                giveHealth = false;
                levelCheck++;
            }
        }
        */

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
            Camera.main.GetComponent<AudioSource>().PlayOneShot(healSFX);
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GetComponent<SaveSystem>().Load();
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
    
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetMaxExp()
    {
        return maxExp;
    }

    public int GetCurrentExp()
    {
        return currentExp;
    }

    public void DisableAutoHeal()
    {
        autoHealTimer = 0;
        canSetTempHealth = true;
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
