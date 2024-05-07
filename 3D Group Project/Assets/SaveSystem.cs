using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private float textDuration = 1;

    private static readonly string keyWord = "password";

    private PlayerExp playerExp;

    private Mage mage;
    private Warrior warrior;
    private Ranger ranger;
    private Enemy enemy;

    private void Awake()
    {
        if (CompareTag("Player"))
        {
            playerExp = GetComponent<PlayerExp>();

            if (TryGetComponent(out mage))
            {
                mage = GetComponent<Mage>();
            }
            else if (TryGetComponent(out warrior))
            {
                warrior = GetComponent<Warrior>();
            }
            else if (TryGetComponent(out ranger))
            {
                ranger = GetComponent<Ranger>();
            }
        }
        else if (CompareTag("Enemy"))
        {
            enemy = GetComponent<Enemy>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        SaveData myData = new();
        if (CompareTag("Player"))
        {
            if (TryGetComponent(out mage))
            {
                myData.currentHealth = mage.GetCurrentHealth();
                myData.maxHealth = mage.GetMaxHealth();
                myData.maxExp = mage.GetMaxExp();
                myData.currentLevel = mage.GetCurrentLevel();
                myData.currentExp = mage.GetCurrentExp();
            }
            else if (TryGetComponent(out warrior))
            {
                myData.currentHealth = warrior.GetCurrentHealth();
                myData.maxHealth = warrior.GetMaxHealth();
                myData.maxExp = warrior.GetMaxExp();
                myData.currentLevel = warrior.GetCurrentLevel();
                myData.currentExp = warrior.GetCurrentExp();
            }
            else if (TryGetComponent(out ranger))
            {
                myData.currentHealth = ranger.GetCurrentHealth();
                myData.maxHealth = ranger.GetMaxHealth();
                myData.maxExp = ranger.GetMaxExp();
                myData.currentLevel = ranger.GetCurrentLevel();
                myData.currentExp = ranger.GetCurrentExp();
            }
        }
        else if (CompareTag("Enemy"))
        {
            myData.currentHealth = enemy.GetCurrentHealth();
            myData.maxHealth = enemy.GetMaxHealth();
        }
        myData.x = transform.position.x;
        myData.y = transform.position.y;
        myData.z = transform.position.z;
        string myDataString = JsonUtility.ToJson(myData);
        myDataString = EncryptDecryptData(myDataString);
        string file = $"{Application.persistentDataPath}/{name}.json";
        File.WriteAllText(file, myDataString);
        //Debug.Log(Application.persistentDataPath);
        progressText.text = "Progress saved!";
        StartCoroutine(ShowProgressText(textDuration));
    }

    public void Load()
    {
        string file = $"{Application.persistentDataPath}/{name}.json";
        if (File.Exists(file))
        {
            string jsonData = File.ReadAllText(file);
            jsonData = EncryptDecryptData(jsonData);
            SaveData myData = JsonUtility.FromJson<SaveData>(jsonData);
            transform.position = new Vector3(myData.x, myData.y, myData.z);
            if (CompareTag("Player"))
            {
                if (TryGetComponent(out mage))
                {
                    mage.DisableAutoHeal();
                    mage.SetCurrentHealth(myData.currentHealth);
                    mage.SetMaxHealth(myData.maxHealth);
                }
                else if (TryGetComponent(out warrior))
                {
                    warrior.DisableAutoHeal();
                    warrior.SetCurrentHealth(myData.currentHealth);
                    warrior.SetMaxHealth(myData.maxHealth);
                }
                else if (TryGetComponent(out ranger))
                {
                    ranger.DisableAutoHeal();
                    ranger.SetCurrentHealth(myData.currentHealth);
                    ranger.SetMaxHealth(myData.maxHealth);
                }
                playerExp.MaxExp = myData.maxExp;
                playerExp.CurrentExp = myData.currentExp;
                playerExp.CurrentLevel = myData.currentLevel;
            }
            else if (CompareTag("Enemy"))
            {
                myData.currentHealth = enemy.GetCurrentHealth();
                myData.maxHealth = enemy.GetMaxHealth();
            }
            progressText.text = "Progress loaded!";
            StartCoroutine(ShowProgressText(textDuration));
        }
    }

    public string EncryptDecryptData(string data)
    {
        string result = string.Empty;

        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ (keyWord[i % keyWord.Length]));
        }

        return result;
    }

    private IEnumerator ShowProgressText(float duration)
    {
        progressText.enabled = true;

        yield return new WaitForSeconds(duration);

        progressText.enabled = false;
    }
}

[Serializable]
public class SaveData
{
    public float x, y, z;
    public int currentExp, maxExp, currentLevel;
    public float currentHealth, maxHealth;
}
