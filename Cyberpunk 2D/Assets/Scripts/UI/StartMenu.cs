using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource _clickingSound;

    public void Play()
    {
        _clickingSound.Play();
        StartLevel.Load();
    }

    public void Quit()
    {
        _clickingSound.Play();
        Application.Quit();
    }
}
