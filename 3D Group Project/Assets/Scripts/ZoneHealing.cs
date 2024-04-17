using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoneHealing : MonoBehaviour
{
    [SerializeField] private bool canSpawnZone = true;
    [Min(0), SerializeField] private float zoneDistance = 1;
    [Min(0), SerializeField] private float zoneLifeTime = 1;
    [Min(0), SerializeField] private float healAmount = 1;
    [Min(0), SerializeField] private float healInterval = 1;
    [Min(0), SerializeField] private float zoneSpawnCooldown = 1;
    [SerializeField] private GameObject zoneSpherePrefab;
    [SerializeField] private Slider zoneCooldownBar;
    [SerializeField] private TextMeshProUGUI cooldownText;

    //Internal Variables
    private bool isCoolingDown = false, isInUse = false;
    private float tempCooldown = 0, tempLifeTime = 0;

    private void Start()
    {
        zoneCooldownBar.maxValue = zoneSpawnCooldown;
        zoneCooldownBar.value = zoneCooldownBar.maxValue;

        tempCooldown = zoneSpawnCooldown;
        tempLifeTime = zoneLifeTime;
    }

    private void Update()
    {
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.R) && !isCoolingDown && canSpawnZone)
        {
            StartCoroutine(SpawnZone());
        }
    }

    private void UpdateUI()
    {
        if (isInUse)
        {
            cooldownText.text = $"In Use: {(int)(tempLifeTime -= Time.deltaTime)}";
        }
        else
        {
            tempLifeTime = zoneLifeTime;
        }

        if (zoneCooldownBar.value < zoneCooldownBar.maxValue && isCoolingDown)
        {
            zoneCooldownBar.value += Time.deltaTime;
            cooldownText.text = $"Cooling Down: {(int)(tempCooldown -= Time.deltaTime)}";
        }
        else
        {
            tempCooldown = zoneSpawnCooldown;
            if (!isInUse)
            {
                cooldownText.text = "Ready To Use";
            }
        }
    }

    private IEnumerator SpawnZone()
    {
        isInUse = true;
        zoneCooldownBar.value = 0;
        GameObject zoneClone = Instantiate(zoneSpherePrefab, transform.position, Quaternion.identity);
        ParticleSystem zoneParticles = zoneClone.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule particleShape = zoneParticles.shape;
        HealZone healZone = zoneClone.GetComponent<HealZone>();

        healZone.SetHealInterval(healInterval);
        healZone.SetHealAmount(healAmount);
        zoneClone.transform.localScale *= zoneDistance;
        zoneParticles.Play();

        yield return new WaitForSeconds(zoneLifeTime);

        Destroy(zoneClone);
        isInUse = false;
        isCoolingDown = true;

        yield return new WaitForSeconds(zoneSpawnCooldown);

        isCoolingDown = false;
    }
}
