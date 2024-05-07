using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI factionText, classText;

    private string factionNameKey = "FactionName", classNameKey = "ClassName";

    public void UpdateFaction(string factionName)
    {
        PlayerPrefs.SetString(factionNameKey, factionName);
    }

    public void UpdateClass(string className)
    {
        PlayerPrefs.SetString(classNameKey, className);
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            factionText.text = $"Faction: {PlayerPrefs.GetString(factionNameKey)}";
            classText.text = $"Class: {PlayerPrefs.GetString(classNameKey)}";
        }
    }

    public void LoadAScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadAScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        /*restart from last save
        SaveSystem saveSystem = GameObject.FindWithTag("Player").GetComponent<SaveSystem>();
        saveSystem.Load();
        */
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void EnableCanvas(Canvas canvas)
    {
        canvas.enabled = true;
    }

    public void DisableCanvas(Canvas canvas)
    {
        canvas.enabled = false;
    }

    public void PlayerToSpawn(int classNumber)
    {
        PlayerPrefs.SetInt("PlayerToSpawn", classNumber);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(audioClip);
    }
}
