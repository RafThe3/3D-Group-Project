using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ConquerLands : MonoBehaviour
{
    [HideInInspector] public int landsConquered = 0;
    [SerializeField] private TextMeshProUGUI interactText;

    private TextMeshProUGUI objectiveText;

    private void Awake()
    {
        landsConquered = 0;
    }

    // Start is called before the first frame update
    private void Start()
    {
        interactText.enabled = false;
        objectiveText = GameObject.Find("Objective Text").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (landsConquered >= 4)
        {
            print("yippie");
            interactText.enabled = true;
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                FindObjectOfType<GameManager>().LoadAScene("End Screen");
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void UpdateUI()
    {
        landsConquered++;
        objectiveText.text = $"Objective: Conquer all the lands ({landsConquered} / 4)";
    }
}
