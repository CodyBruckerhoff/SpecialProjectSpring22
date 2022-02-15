using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer gameMixer;

    public void setMusicVolume (float volume)
    {
        musicMixer.SetFloat("MusicVolume", volume);
    }

    public void setGameVolume(float volume)
    {
        gameMixer.SetFloat("GameVolume", volume);
    }
}
