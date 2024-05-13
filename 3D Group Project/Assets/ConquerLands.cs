using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConquerLands : MonoBehaviour
{
    [HideInInspector] public int landsConquered = 0;
    private TextMeshProUGUI objectiveText;

    private bool desertSpawn, snowSpawn, volcanoSpawn, kingdomSpawn;

    // Start is called before the first frame update
    private void Start()
    {
        objectiveText = GameObject.Find("Objective Text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUI()
    {
        landsConquered++;
        objectiveText.text = $"Objective: Conquer all the lands ({landsConquered} / 4)";
    }
}
