using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaundController : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip soundClip;
    public bool playOnAwake = false;

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        audioSource.clip = soundClip;
        audioSource.loop = true;

        if (playOnAwake)
        {
            PlaySound();
        }
    }

    public void SetSoundClip(AudioClip newClip)
    {
        soundClip = newClip;
        audioSource.clip = newClip;
    }

    public void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void SetVolume(float volumeLevel)
    {
        audioSource.volume = Mathf.Clamp01(volumeLevel);
    }
}
