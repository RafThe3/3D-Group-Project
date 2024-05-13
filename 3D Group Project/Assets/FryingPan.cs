using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FryingPan : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private float pickupDistance = 1;

    private void Start()
    {
        interactText.enabled = false;
        interactText.text = "Press E to pickup";
    }

    private void Update()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position - transform.position;
        bool isPlayerNear = playerPos.magnitude < pickupDistance;

        if (isPlayerNear && Time.timeScale > 0)
        {
            interactText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E) && Time.timeScale > 0)
            {
                FindObjectOfType<ItemSwitch>().EnableItem2();
                interactText.enabled = false;
                Destroy(gameObject);
            }
        }
        else
        {
            interactText.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pickupDistance);
    }
}
