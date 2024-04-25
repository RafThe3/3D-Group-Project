using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage = 1;

    private void Start()
    {
        damage = FindObjectOfType<Shooting>().GetDamage();
    }

    /*
    private void Update()
    {
        Ray hitDirection = new(transform.position, transform.forward);

        if (Physics.Raycast(hitDirection, out RaycastHit hit, 10) && hit.collider.CompareTag("Enemy"))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DamageEnemy(other);
        }
    }

    private void DamageEnemy(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
