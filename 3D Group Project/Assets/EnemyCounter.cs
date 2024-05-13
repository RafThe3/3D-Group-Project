using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TextMeshProUGUI enemiesRemainingText;

    private int enemiesToSpawn, enemiesRemaining;
    private bool conquerLand = true;
    private ConquerLands conquerLands;

    // Start is called before the first frame update
    private void Start()
    {
        enemiesRemainingText.enabled = false;
        enemiesRemaining = enemiesToSpawn;
        conquerLands = FindObjectOfType<ConquerLands>();
    }

    // Update is called once per frame
    private void Update()
    {
        enemiesRemainingText.text = $"Enemies Remaining: {enemiesRemaining}";
        if (enemiesRemaining <= 0 && conquerLand)
        {
            conquerLands.UpdateUI();
            conquerLand = false;
            GetComponent<Spawner>().enabled = false;
        }
    }

    public void UpdateEnemiesToSpawn(int enemies)
    {
        enemiesToSpawn = enemies;
    }

    public void SubtractEnemiesRemaining()
    {
        enemiesRemaining--;
    }
}
