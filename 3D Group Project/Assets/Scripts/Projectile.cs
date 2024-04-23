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
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                DamagePlayer(other);
                Destroy(gameObject);
            }
        }
    }

    private void DamagePlayer(Collider player)
    {
        if (player.TryGetComponent(out Mage _))
        {
            player.GetComponent<Mage>().TakeDamage(damage);
        }
        else if (player.TryGetComponent(out Warrior _))
        {
            player.GetComponent<Warrior>().TakeDamage(damage);
        }
        else if (player.TryGetComponent(out Ranger _))
        {
            player.GetComponent<Ranger>().TakeDamage(damage);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
