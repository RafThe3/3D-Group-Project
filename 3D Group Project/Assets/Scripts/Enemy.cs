using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private bool isInvincible = false;
    [Min(0), SerializeField] private float startingHealth = 1;
    [Min(0), SerializeField] private float maxHealth = 100;

    [Header("Attacking")]
    [SerializeField] private AttackType attackType = AttackType.None;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 1;
    [SerializeField] private float projectileLife = 1;
    [SerializeField] private float shootInterval = 1;
    //[SerializeField] private Transform projectileSpawnPoint;

    [Header("Melee")]
    [Min(0), SerializeField] private float meleeInterval = 1;

    //Internal Variables
    private NavMeshAgent agent;
    private float currentHealth = 0;
    private Slider healthBar;
    private Canvas enemyUI;
    private EnemyStats enemyStats;
    private float meleeTimer = 0, shootTimer = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStats = FindObjectOfType<EnemyStats>();
    }

    private void Start()
    {
        if (startingHealth > maxHealth)
        {
            startingHealth = maxHealth;
        }
        currentHealth = startingHealth;
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<Slider>();
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = startingHealth;
        enemyUI = GetComponentInChildren<Canvas>();
        enemyUI.enabled = false;
        meleeTimer = meleeInterval;
        shootTimer = shootInterval;
    }

    private void Update()
    {
        meleeTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        MoveEnemy();

        if (attackType == AttackType.Shoot && shootTimer >= shootInterval)
        {
            Shoot();
        }
    }

    private void MoveEnemy()
    {
        GameObject player = GameObject.FindWithTag("Player");
        agent.destination = player.transform.position;
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && meleeTimer >= meleeInterval && attackType == AttackType.Melee)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(enemyStats.enemyDamage);
            meleeTimer = 0;
        }
    }

    private void Shoot()
    {
        GameObject projectileClone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindWithTag("Player");
        projectileClone.GetComponent<Rigidbody>().velocity = 10 * projectileSpeed * player.transform.position;
        shootTimer = 0;
        Destroy(projectileClone, projectileLife);
    }

    private enum AttackType { None, Melee, Shoot }
}
