using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class to handle the introduction narration audio
 */
public class MirrorSceneAudio : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Subtitles subtitles;
    private int counter;
    private AudioSource audioSrc;
    private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        counter = 0;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerScript.timer > 3 && !isPlaying) //Starts narration after 3 seconds
        {   
            isPlaying = true;
            StartNarration();
        }
        
    }

    /**
     * Begins the narration coroutine
     */
    private void StartNarration()
    {
        StartCoroutine(BathroomNarration("bathroomNarration"));
    }

    /**
     * Coroutine to play the narration audio array. Audio split into an array to
     * enable subtitling.
     * @param daloge array to be played
     */
    IEnumerator BathroomNarration(string dialogue)
    {
        while (counter < audioManager.ConversationArrayLength(dialogue))
        {
            audioManager.NpcConversation(dialogue, counter, gameObject);
            subtitles.DisplaySubtitleArray(dialogue, counter);
            yield return new WaitForSeconds(audioSrc.clip.length);
            counter++;
        }
        subtitles.HideSubtitle();
        counter = 0;        
    }
}
