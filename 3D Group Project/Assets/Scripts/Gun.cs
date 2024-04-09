using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool canShoot = true;

    [Header("Projectile Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 1;
    [SerializeField] private float bulletLife = 1;

    [Header("Line Shooting")]
    [Min(0), SerializeField] private int damageAmount = 1;
    [Min(0), SerializeField] private float maxDistance = 1;

    private void Update()
    {
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            Shoot();
            //SpawnObject();
        }
    }

    private void SpawnObject()
    {
        GameObject bulletClone = Instantiate(bulletPrefab, Camera.main.transform.position, Quaternion.identity);
        bulletClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(10 * bulletSpeed * Camera.main.transform.forward);
        Destroy(bulletClone, bulletLife);
    }

    private void Shoot()
    {
        Ray shootDirection = new(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(shootDirection, out RaycastHit hit, maxDistance) && hit.collider.CompareTag("Enemy"))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.TakeDamage(damageAmount);
        }
    }
}
