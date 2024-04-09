using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    private Canvas playerUi;

    // Start is called before the first frame update
    private void Awake()
    {
        playerUi = GetComponent<Canvas>();
        playerUi.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerUi.enabled = !playerUi.enabled;
        }
    }
}
