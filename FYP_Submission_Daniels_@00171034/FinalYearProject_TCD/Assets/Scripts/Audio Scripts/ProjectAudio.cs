using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Enable class to be viewed in inspector
/**
 * Class to hold audio clips that can be used by the audio manager.
 */
public class ProjectAudio 
{
    public AudioClip audioClip;

    public string name;
    [Range(0f,1f)]
    public float Volume;    
    public bool loop;
    //[HideInInspector]
    private AudioSource audioSource;
    

    /**
     * Sets the audio source for the clip
     */
    public void setAudioSource(AudioSource source)
    {
        audioSource = source;
    }

    /**
     * Returns the audio source 
     */
    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
}
