using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script to handle letter movement through front door and related audio
 */
public class LetterScript : MonoBehaviour
{
    public bool sendLetter;
    public bool disableAnimator;
    private bool letterBoxAudioPlayed;
    private bool laughterAudioPlayed;    
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {        
        sendLetter = false;
        letterBoxAudioPlayed = false;
        laughterAudioPlayed = false;
        animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (sendLetter)
            MoveLetter();       

    }

    /**
     * Sets send letter bool to tru to enable the letter to be sent.
     */
    public void SendLetter()
    {
        sendLetter = true;
    }

    /**
     * Starts custom animation which moves the letter though the door, plays letterbox sound.
     */
    private void MoveLetter()
    {
        
        if(animator != null)
            animator.SetTrigger("SendLetter");        
        
        sendLetter = false;       
        
        if (!letterBoxAudioPlayed)
        {
            FindObjectOfType<AudioManager>().PlayOnObject("LetterBox", gameObject);//Play audio from audiomanager class
            letterBoxAudioPlayed = true;
        }
    }

    /**
     * Disables the animator, this is called through an event at the end of the animation. Enables letter to be picked up.
     */
    public void DisableAnimator()
    {
        animator.enabled = false; 
    }

    /**
     * Trigger script to play a laughter sound when letter is dicovered
     */
    private void OnTriggerEnter(Collider other)
    {
        if (!laughterAudioPlayed && other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlayOnObject("LetterLaughter", gameObject);//Play audio from audiomanager class
            laughterAudioPlayed = true;
        }
    }
}
