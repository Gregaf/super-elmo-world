﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [Range(0,1)]
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float fadeTime = 1f;

    [Header("Sfx")]
    [Range(0, 1)]
    [SerializeField] private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError($"There is already an instance of {this}");

        // musicSource = this.GetComponent<AudioSource>();
        // sfxSource = this.GetComponent<AudioSource>();

        sfxSource.volume = this.sfxVolume;
    }

    public void PlaySingleSfx(AudioClip sfxAudioClip)
    {
        sfxSource.pitch = 1f;

        sfxSource.PlayOneShot(sfxAudioClip);
    }

    public void PlaySingleRandomSfx(AudioClip sfxAudioClip)
    {
        float rand = Random.Range(0.9f, 1.1f);

        sfxSource.pitch = rand;

        sfxSource.PlayOneShot(sfxAudioClip);
    }


    public void PlayTrack(AudioClip trackToPlay)
    {
        musicSource.volume = 0f;
        musicSource.PlayOneShot(trackToPlay);
        
        StartCoroutine(FadeMusic(0, this.musicVolume));
    }

    public void StopTrack()
    {
        StartCoroutine(FadeMusic(this.musicVolume, 0));


    }




    private IEnumerator FadeMusic(float beginning, float ending)
    {
        float fadeValue = 0;

        while (musicSource.volume <= musicVolume)
        {
            fadeValue += Time.deltaTime;

            musicSource.volume = Mathf.Lerp(beginning, ending, fadeValue / fadeTime);


            yield return null;
        }


        yield return null;
    }
}
