using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private bool isTargetingPlayer = false;

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
        if (!isTargetingPlayer)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
