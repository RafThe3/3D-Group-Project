using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [Min(0), SerializeField] private float attackDistance = 1;
    [Min(0), SerializeField] private float attackInterval = 1;

    //Internal Variables
    private WeaponStats weaponStats;
    private float attackTimer = 0;

    private void Awake()
    {
        weaponStats = FindObjectOfType<WeaponStats>();
    }

    private void Start()
    {
        attackTimer = attackInterval;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && attackTimer >= attackInterval)
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
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
        }
    }
}
