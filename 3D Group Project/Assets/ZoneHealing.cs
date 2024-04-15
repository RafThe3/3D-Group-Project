using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneHealing : MonoBehaviour
{
    [Min(0), SerializeField] private float zoneDistance = 1;
    [Min(0), SerializeField] private float zoneLifeTime = 1;
    [Min(0), SerializeField] private float healAmount = 1;
    [Min(0), SerializeField] private float healInterval = 1;
    [SerializeField] private GameObject zoneSpherePrefab;

    private float healTimer = 0;

    // Start is called before the first frame update
    private void Start()
    {
        ParticleSystem zoneParticles = zoneSpherePrefab.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule particleShape = zoneParticles.shape;

        zoneSpherePrefab.transform.localScale *= zoneDistance;
        particleShape.radius = zoneDistance;
    }

    // Update is called once per frame
    private void Update()
    {
        healTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(SpawnZone());
        }
    }

    private IEnumerator SpawnZone()
    {
        GameObject zoneClone = Instantiate(zoneSpherePrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(zoneLifeTime);

        Destroy(zoneClone);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(zoneSpherePrefab.transform.position, zoneDistance);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && healTimer >= healInterval)
        {
            Player player = other.GetComponent<Player>();
            player.Heal(healAmount);
            healTimer = 0;
        }
    }
}
