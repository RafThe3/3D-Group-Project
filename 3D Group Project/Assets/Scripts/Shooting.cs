using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [Header("General Settings"), Space]
    [SerializeField] private bool canShoot = true;
    [SerializeField] private ShootType shootType = ShootType.Line;
    [Min(0), SerializeField] private int damageAmount = 1;
    [Min(0), SerializeField] private float shootCooldown = 1;
    [SerializeField] private Slider shootCooldownBar;

    [Header("Projectile Shooting"), Space]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 1;
    [SerializeField] private float projectileLife = 1;
    [SerializeField] private Transform projectileSpawnPoint;

    [Header("Line Shooting"), Space]
    [Min(0), SerializeField] private float maxDistance = 1;

    //Internal Variables
    private float shootTimer = 0;

    //ammo needed

    private void Start()
    {
        shootTimer = shootCooldown;
        shootCooldownBar.maxValue = shootCooldown;
        shootCooldownBar.value = shootCooldownBar.maxValue;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;

        bool isCoolingDown = shootCooldownBar.value < shootCooldownBar.maxValue;
        shootCooldownBar.gameObject.SetActive(isCoolingDown);

        if (isCoolingDown)
        {
            shootCooldownBar.value += Time.deltaTime;
        }

        bool isShooting = Input.GetButtonDown("Fire1") && shootTimer >= shootCooldown;
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
        GameObject projectileClone = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectileClone.GetComponent<Rigidbody>().velocity = 10 * projectileSpeed * Camera.main.transform.forward;
        shootTimer = 0;
        shootCooldownBar.value = 0;
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
            shootCooldownBar.value = 0;
        }
    }

    public int GetDamage()
    {
        return damageAmount;
    }

    private enum ShootType { Projectile, Line }
}
