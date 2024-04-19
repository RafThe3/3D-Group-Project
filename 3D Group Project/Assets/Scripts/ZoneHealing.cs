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
    [Min(1), SerializeField] private float healInterval = 1;
    [Min(0), SerializeField] private float zoneSpawnCooldown = 1;
    [SerializeField] private GameObject zoneSpherePrefab;
    [SerializeField] private Slider zoneCooldownBar;
    [SerializeField] private TextMeshProUGUI cooldownText;

    //Internal Variables
    private bool isCoolingDown = false, isInUse = false;
    private float tempCooldown = 0, tempLifeTime = 0, tempLifeTime2 = 0;
    private bool tempCanSpawnZone = false;
    private MageClassStats mageClass;

    private void Awake()
    {
        mageClass = FindObjectOfType<MageClassStats>();
    }

    private void Start()
    {
        healAmount = mageClass.MageHealing;
        zoneCooldownBar.maxValue = zoneSpawnCooldown;
        zoneCooldownBar.value = zoneCooldownBar.maxValue;

        tempCooldown = zoneSpawnCooldown;
        tempLifeTime = zoneLifeTime;
        tempLifeTime2 = zoneLifeTime;
        tempCanSpawnZone = canSpawnZone;
    }

    private void Update()
    {
        if (tempCanSpawnZone)
        {
            canSpawnZone = Time.timeScale > 0;
        }

        healAmount = mageClass.MageHealing;
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
            zoneCooldownBar.maxValue = zoneLifeTime;
            zoneCooldownBar.value = tempLifeTime2 -= Time.deltaTime;
        }
        else
        {
            tempLifeTime = zoneLifeTime;
            tempLifeTime2 = zoneLifeTime;
            zoneCooldownBar.maxValue = zoneSpawnCooldown;
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
