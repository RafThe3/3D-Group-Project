using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneHealing : MonoBehaviour
{
    [SerializeField] private bool canSpawnZone = true;
    [Min(0), SerializeField] private float zoneDistance = 1;
    [Min(0), SerializeField] private float zoneLifeTime = 1;
    [Min(0), SerializeField] private float healAmount = 1;
    [Min(0), SerializeField] private float healInterval = 1;
    [Min(0), SerializeField] private float zoneSpawnCooldown = 1;
    [SerializeField] private GameObject zoneSpherePrefab;
    [SerializeField] private Slider zoneSpawnBar;

    private bool isCoolingDown = false;

    private void Start()
    {
        zoneSpawnBar.maxValue = zoneSpawnCooldown;
        zoneSpawnBar.value = zoneSpawnBar.maxValue;
    }

    private void Update()
    {
        if (zoneSpawnBar.value < zoneSpawnBar.maxValue && isCoolingDown)
        {
            zoneSpawnBar.value += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isCoolingDown)
        {
            StartCoroutine(SpawnZone());
        }
    }

    private IEnumerator SpawnZone()
    {
        zoneSpawnBar.value = 0;
        GameObject zoneClone = Instantiate(zoneSpherePrefab, transform.position, Quaternion.identity);
        ParticleSystem zoneParticles = zoneClone.GetComponent<ParticleSystem>();
        HealZone healZone = zoneClone.GetComponent<HealZone>();

        healZone.SetHealInterval(healInterval);
        healZone.SetHealAmount(healAmount);
        zoneClone.transform.localScale *= zoneDistance;
        zoneParticles.Play();

        yield return new WaitForSeconds(zoneLifeTime);

        Destroy(zoneClone);
        isCoolingDown = true;

        yield return new WaitForSeconds(zoneSpawnCooldown);

        isCoolingDown = false;
    }
}
