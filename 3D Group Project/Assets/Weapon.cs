using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //private WeaponStats weaponStats;

    [SerializeField] private GameObject attackPoint;
    [Min(0), SerializeField] private float attackDistance = 1;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackDistance, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            //enemyHealth.TakeDamage(damageAmount);
            //PlaySFX(damageSFX);
        }
        //animator.SetTrigger("Attack");
        //timer = 0;
    }
}
