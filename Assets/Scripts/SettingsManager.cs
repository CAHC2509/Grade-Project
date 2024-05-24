using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float volume) => audioMixer.SetFloat("masterVolume", volume);
    public void SetMusicVolume(float volume) => audioMixer.SetFloat("musicVolume", volume);
    public void SetSFXVolume(float volume) => audioMixer.SetFloat("sfxVolume", volume);
}
