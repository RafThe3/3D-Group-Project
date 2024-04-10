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

    [Header("Attack")]
    [Min(0), SerializeField] private float attackInterval = 1;

    //Internal Variables
    private NavMeshAgent agent;
    private float currentHealth = 0;
    private Slider healthBar;
    private Canvas enemyUI;
    private EnemyStats enemyStats;
    private float attackTimer = 0;

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
        attackTimer = attackInterval;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        MoveEnemy();
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
        if (collision.gameObject.CompareTag("Player") && attackTimer >= attackInterval)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(enemyStats.enemyDamage);
            attackTimer = 0;
        }
    }
}
