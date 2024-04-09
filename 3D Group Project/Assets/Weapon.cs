using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [Min(0), SerializeField] private float attackDistance = 1;
    [Min(0), SerializeField] private float attackInterval = 1;
    [Min(0), SerializeField] private float damage = 1;

    //Internal Variables
    private WeaponStats weaponStats;
    private float timer = 0;

    private void Start()
    {
        timer = attackInterval;
        damage = weaponStats.WepDamage;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && timer >= attackInterval)
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
            enemyHealth.TakeDamage(damage);
            //PlaySFX(damageSFX);
        }
        //animator.SetTrigger("Attack");
        timer = 0;
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
        }
    }
}
