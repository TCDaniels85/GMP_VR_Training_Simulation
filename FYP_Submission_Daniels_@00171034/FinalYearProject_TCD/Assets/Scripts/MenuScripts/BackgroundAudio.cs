using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Plays background audio for menu and results scene
 */
public class BackgroundAudio : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlaySound("BackgroundMusic");  
    }

    
}
