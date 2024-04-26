using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [Min(0), SerializeField] private float chaseDistance = 1;

    [Header("Health")]
    [SerializeField] private bool isInvincible = false;
    [Min(0), SerializeField] private float startingHealth = 1;
    [Min(0), SerializeField] private float maxHealth = 100;

    [Header("Attacking")]
    [SerializeField] private AttackType attackType = AttackType.None;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [Min(0), SerializeField] private float projectileSpeed = 1;
    [Min(0), SerializeField] private float projectileLife = 1;
    [Min(0), SerializeField] private float shootInterval = 1;
    [Min(0), SerializeField] private float shootDistance = 1;
    [SerializeField] private AudioClip shootSFX;
    //[SerializeField] private AudioSource audioSource;
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
    private AudioSource audioSource;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStats = FindObjectOfType<EnemyStats>();
        audioSource = GetComponentInChildren<AudioSource>();
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
        if (attackType == AttackType.Shoot)
        {
            Shoot();
        }
    }

    private void MoveEnemy()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPosition = player.transform.position - transform.position;
        if (playerPosition.magnitude < chaseDistance)
        {
            agent.destination = player.transform.position;
        }
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
            float enemyDamage = enemyStats.enemyDamage;
            GameObject player = collision.gameObject;

            AttackPlayer(enemyDamage, player);
            meleeTimer = 0;
        }
    }

    private static void AttackPlayer(float enemyDamage, GameObject player)
    {
        if (player.TryGetComponent(out Mage _))
        {
            player.GetComponent<Mage>().TakeDamage(enemyDamage);
        }
        else if (player.TryGetComponent(out Warrior _))
        {
            player.GetComponent<Warrior>().TakeDamage(enemyDamage);
        }
        else if (player.TryGetComponent(out Ranger _))
        {
            player.GetComponent<Ranger>().TakeDamage(enemyDamage);
        }
    }

    private void Shoot()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPosition = player.transform.position - transform.position;

        if (shootTimer >= shootInterval && playerPosition.magnitude < shootDistance)
        {
            GameObject projectileClone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileClone.GetComponent<Rigidbody>().velocity = (10 * projectileSpeed * playerPosition.normalized) + new Vector3(0, 1.5f);
            shootTimer = 0;
            audioSource.PlayOneShot(shootSFX);
            Destroy(projectileClone, projectileLife);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }

    private enum AttackType { None, Melee, Shoot }
}
