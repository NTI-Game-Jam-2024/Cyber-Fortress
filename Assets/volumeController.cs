using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class volumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("setMusicVolume"))
        {
            loadVolume();
        }
        else
        {
            changeMusicVolume();
            changeSFXVolume();
        }

 
    }
   


    public void changeMusicVolume()
    {
        float musicVolume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(musicVolume)*20);
        PlayerPrefs.SetFloat("setMusicVolume", musicVolume);
    }

    public void changeSFXVolume()
    {
        float SFXVolume = musicSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(SFXVolume) * 20);
        PlayerPrefs.SetFloat("setSFXVolume", SFXVolume);
    }

    private void loadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("setMusicVolume");

        changeMusicVolume();
        changeSFXVolume();

    }


}


