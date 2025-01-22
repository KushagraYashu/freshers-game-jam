using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer masterAudioMixer;

    public TMP_Dropdown qualityDropdown;

    private void Start()
    {
        Debug.Log(QualitySettings.GetQualityLevel());
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetVolume(float volume)
    {
        masterAudioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
