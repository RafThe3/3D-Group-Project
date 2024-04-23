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
            HealPlayer(other);
            healTimer = 0;
        }
    }

    private void HealPlayer(Collider other)
    {
        if (other.TryGetComponent(out Warrior _))
        {
            other.GetComponent<Warrior>().Heal(healAmount);
        }
        else if (other.TryGetComponent(out Ranger _))
        {
            other.GetComponent<Ranger>().Heal(healAmount);
        }
        else if (other.TryGetComponent(out Mage _))
        {
            other.GetComponent<Mage>().Heal(healAmount);
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
