using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip rifleFireEffectClip;
    [SerializeField] private AudioClip huntedDuckSoundClip;
    [SerializeField] private AudioClip notHuntedSoundClip;
    [SerializeField] private AudioClip reloadRifleSoundClip;
    [SerializeField] private AudioClip reloadedRifleSoundClip;


    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReloadingRifle()
    {
        audioSource.PlayOneShot(reloadRifleSoundClip);
    }

    public void ReloadedRifle()
    {
        audioSource.PlayOneShot(reloadedRifleSoundClip);
    }

    public void RifleFireSoundEffect()
    {
        audioSource.PlayOneShot(rifleFireEffectClip);
    }

    public void DuckHuntedSoundEffect()
    {
        audioSource.PlayOneShot(huntedDuckSoundClip);
    }
    public void NotHuntedSoundEffect()
    {
        audioSource.PlayOneShot(notHuntedSoundClip);
    }
}