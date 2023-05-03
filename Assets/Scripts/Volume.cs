using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    float musicLevel;
    float soundLevel;

    void Start()
    {
        if (PlayerPrefs.HasKey("MainMusic"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MainMusic");
            masterMixer.SetFloat("Main", PlayerPrefs.GetFloat("MainMusic"));
        }
        else
        {
            musicSlider.value = 0;
            masterMixer.SetFloat("Main", 0);
        }

        if (PlayerPrefs.HasKey("Effects"))
        {
            soundSlider.value = PlayerPrefs.GetFloat("Effects");
            masterMixer.SetFloat("Sound", PlayerPrefs.GetFloat("Effects"));
        }
        else
        {
            soundSlider.value = 0;
            masterMixer.SetFloat("Sound", 0);
        }
    }

    public void SetMusicLevel()
    {
        musicLevel = musicSlider.value;
        masterMixer.SetFloat("Main", musicLevel);
        PlayerPrefs.SetFloat("MainMusic", musicLevel);
    }

    public void SetSoundLevel()
    {
        soundLevel = soundSlider.value;
        masterMixer.SetFloat("Sound", soundLevel);
        PlayerPrefs.SetFloat("Effects", soundLevel);
    }
}
