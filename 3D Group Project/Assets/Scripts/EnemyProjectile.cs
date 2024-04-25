using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float damage = 1;

    private void Start()
    {
        damage = FindObjectOfType<EnemyStats>().enemyDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DamagePlayer(other);
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
}
