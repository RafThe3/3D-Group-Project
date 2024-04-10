using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private Canvas playerUi;

    private void Start()
    {
        playerUi.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerUi.enabled = !playerUi.enabled;
        }
    }
}
