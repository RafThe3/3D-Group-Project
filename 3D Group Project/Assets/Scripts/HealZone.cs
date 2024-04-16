using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    private float healTimer = 0;
    private float healInterval = 0;
    private float healAmount = 0;

    private void Update()
    {
        healTimer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);

        if (other.CompareTag("Player") && healTimer >= healInterval)
        {
            Player player = other.GetComponent<Player>();
            player.Heal(healAmount);
            healTimer = 0;
        }
    }

    public void SetHealInterval(float time)
    {
        healInterval = time;
    }

    public void SetHealAmount(float health)
    {
        healAmount = health;
    }
}
