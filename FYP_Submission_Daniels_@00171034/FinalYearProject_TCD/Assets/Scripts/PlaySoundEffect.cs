using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    private AudioSource audioSrc;    
    private bool isEffectPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        
    }

   /**
    * Plays sound effect and prevents the sound being started again whilst it is playing
    */
    IEnumerator PlayEffect()
    {
        audioSrc.Play();
        isEffectPlaying = true; //boolean used to prevent sound being played again
        yield return new WaitForSeconds(audioSrc.clip.length);
        isEffectPlaying = false;
    }

    /**
     * Trigger script to play sound effects dependant on what item is being interacted with.
     * @param Collider game object is interacting with
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            
            if(gameObject.tag == "Toilet" || gameObject.tag == "Taps")
            {
                if (!isEffectPlaying)
                {
                    StartCoroutine(PlayEffect());
                }
            }
            
            
            
        }
    }

    /**
     * Used to play colission noise for the ball hitting a surface.
     * @param object that is being collided with
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "RightHand" || collision.gameObject.tag != "LeftHand") //Prevents ball bounce sound when picking ball up
            StartCoroutine(PlayEffect());
    }
}
