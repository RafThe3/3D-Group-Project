using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        pauseMenu.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnablePauseMenu();
        }

        Cursor.lockState = pauseMenu.enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void EnablePauseMenu()
    {
        pauseMenu.enabled = !pauseMenu.enabled;
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        if (audioSource)
        {
            audioSource.Play();
            if (!pauseMenu.enabled)
            {
                audioSource.Stop();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1;
        if (audioSource)
        {
            audioSource.Stop();
        }
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
