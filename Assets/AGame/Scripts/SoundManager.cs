using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip lvl_UP;
    [SerializeField] private AudioClip fail;

    private AudioSource audioSource;

    public void PlayLVL_UP()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().clip = lvl_UP;
        audioSource.Play();
    }

    public void PlayFail()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().clip = fail;
        audioSource.Play();
    }

    public void PlayCorrect()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().clip = correct;
        audioSource.Play();
    }

    public bool IsPlaying()
    {
        audioSource = GetComponent<AudioSource>();
        return audioSource.isPlaying && audioSource.clip == lvl_UP;
    }
}
