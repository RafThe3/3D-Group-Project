using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [Min(0), SerializeField] private float attackDistance = 1;
    [Min(0), SerializeField] private float attackCooldown = 1;
    [SerializeField] private Slider attackCooldownBar;

    //Internal Variables
    private WeaponStats weaponStats;
    private float attackTimer = 0;

    private void Awake()
    {
        weaponStats = FindObjectOfType<WeaponStats>();
    }

    private void Start()
    {
        attackTimer = attackCooldown;
        attackCooldownBar.maxValue = attackCooldown;
        attackCooldownBar.value = attackCooldownBar.maxValue;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        bool isCoolingDown = attackCooldownBar.value < attackCooldownBar.maxValue;
        attackCooldownBar.gameObject.SetActive(isCoolingDown);

        if (isCoolingDown)
        {
            attackCooldownBar.value += Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && attackTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackDistance, LayerMask.GetMask("Enemy"));

        foreach (Collider enemy in enemies)
        {
            Enemy enemyHealth = enemy.GetComponent<Enemy>();
            enemyHealth.TakeDamage(weaponStats.WepDamage);
            //PlaySFX(damageSFX);
        }
        //animator.SetTrigger("Attack");
        attackTimer = 0;
        attackCooldownBar.value = 0;
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
        }
    }
}
