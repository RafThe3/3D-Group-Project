using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider slider;
    [SerializeField] private TMPro.TextMeshProUGUI fovText;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("FOV");
    }

    private void Update()
    {
        ChangeFOV();
    }

    private void ChangeFOV()
    {
        fovText.text = $"{Camera.main.fieldOfView}";
        PlayerPrefs.SetFloat("FOV", Camera.main.fieldOfView);
    }

    public void AddSliderValue()
    {
        slider.value++;
    }

    public void DecreaseSliderValue()
    {
        slider.value--;
    }
}
