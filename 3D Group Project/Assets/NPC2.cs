using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC2 : MonoBehaviour
{
    [SerializeField] private Canvas ui;
    [SerializeField] private float interactDistance = 1;
    [SerializeField] private TextMeshProUGUI interactText;

    private int interaction = 0;

    // Start is called before the first frame update
    private void Start()
    {
        ui.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPos = player.transform.position - transform.position;
        bool isPlayerNear = playerPos.magnitude < interactDistance;

        if (ui.enabled && !pauseMenu.AutoLockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (isPlayerNear)
        {
            interactText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction++;
                if (interaction > 1)
                {
                    interaction = 0;
                    pauseMenu.AutoLockCursor = true;
                }
                pauseMenu.AutoLockCursor = false;
                ui.enabled = interaction == 1;
                Time.timeScale = interaction == 1 ? 0 : 1;
            }
        }
        else
        {
            interactText.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}
