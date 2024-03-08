using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource laser;
    public AudioSource brick;
    public AudioSource explosion;
    public AudioSource electric;
    public AudioSource poweup;
    public GameObject defeatScreen;
    public GameObject timer;

    public void Defeat()
    {
        timer.SetActive(false);
        defeatScreen.SetActive(true);
    }
}