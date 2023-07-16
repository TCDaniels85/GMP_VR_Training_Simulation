using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Enable class to be viewed in inspector
/**
 * Class to hold audio objects for 3D sounds
 */
public class Audio3D 
{

    public AudioClip audioClip;

    public string clipName;
    [Range(0f, 1f)]
    public float Volume;
    [Range(0f, 1f)]
    public float spatialBland;
    public float minDistance = 1;
    public float maxDistance = 500;
    public bool loop;
    
}
