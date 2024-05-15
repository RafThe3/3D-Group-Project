using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private AudioSource pauseMenuAudioSource;
    [SerializeField] private AudioSource[] audioSourcesToMute;

    public bool AutoLockCursor { get; set; } = true;

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

        if (AutoLockCursor)
        {
            Cursor.lockState = pauseMenu.enabled ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void EnablePauseMenu()
    {
        pauseMenu.enabled = !pauseMenu.enabled;
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        if (pauseMenuAudioSource)
        {
            pauseMenuAudioSource.Play();
            PlayAudios(true);
            if (!pauseMenu.enabled)
            {
                pauseMenuAudioSource.Stop();
                PlayAudios(false);
            }
        }
    }

    private void PlayAudios(bool shouldPlay)
    {
        for (int i = 0; i < audioSourcesToMute.Length; i++)
        {
            if (shouldPlay)
            {
                audioSourcesToMute[i].Pause();
            }
            else
            {
                audioSourcesToMute[i].Play();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1;
        if (pauseMenuAudioSource)
        {
            pauseMenuAudioSource.Stop();
            PlayAudios(false);
        }
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
