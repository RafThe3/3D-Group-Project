using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool canShoot = true;
    [Min(0), SerializeField] private int damageAmount = 1;

    private void Update()
    {
        if (!canShoot)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Ray shootDirection = new(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(shootDirection, out RaycastHit hit) && hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
}
