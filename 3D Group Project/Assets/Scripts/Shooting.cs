using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private bool canShoot = true;
    [Min(0), SerializeField] private int damageAmount = 1;
    [SerializeField] private ShootType shootType = ShootType.Line;

    [Header("Projectile Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 1;
    [SerializeField] private float projectileLife = 1;
    [SerializeField] private Transform projectileSpawnPoint;

    [Header("Line Shooting")]
    [Min(0), SerializeField] private float maxDistance = 1;
    [Min(0), SerializeField] private float shootInterval = 1;

    //Internal Variables
    private float shootTimer = 0;

    private void Start()
    {
        shootTimer = shootInterval;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;

        bool isShooting = Input.GetButtonDown("Fire1") && shootTimer >= shootInterval;
        if (canShoot && isShooting)
        {
            switch (shootType)
            {
                case ShootType.Projectile:
                    SpawnProjectile();
                    break;
                case ShootType.Line:
                    ShootLine();
                    break;
            }
        }
    }

    private void SpawnProjectile()
    {
        GameObject projectileClone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileClone.GetComponent<Rigidbody>().velocity = 10 * projectileSpeed * Camera.main.transform.forward;
        shootTimer = 0;
        Destroy(projectileClone, projectileLife);
    }

    private void ShootLine()
    {
        Ray shootDirection = new(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(shootDirection, out RaycastHit hit, maxDistance) && hit.collider.CompareTag("Enemy"))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.TakeDamage(damageAmount);
            shootTimer = 0;
        }
    }

    public int GetDamage()
    {
        return damageAmount;
    }

    private enum ShootType { Projectile, Line }
}
