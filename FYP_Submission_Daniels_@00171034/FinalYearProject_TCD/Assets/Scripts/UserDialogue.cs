using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Handles the dialogue audio for the user character. 
 */
public class UserDialogue : MonoBehaviour
{

    [SerializeField] private InputAction primaryPress;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Subtitles subtitles;
    private AudioSource audioSrc;    
    public static bool audioPlaying;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    
    /**
     * Plays a line of dialogue, used for providing hints in scene.
     * @param audio clip name
     */
    public void PlayDialogueHint(string audioDialogueRef)
    {
        StartCoroutine(InternalMonologue(audioDialogueRef));
    }

    /**
     * coroutine to play a line of dialogue from the audio manager
     * @param name of clip to be played
     */
    IEnumerator InternalMonologue(string clipName)
    {
        audioPlaying = true;
        audioManager.PlayOnObject(clipName, gameObject);
        subtitles.DisplaySubtitle(clipName);
        Debug.Log(audioSrc.clip.length);
        yield return new WaitForSeconds(audioSrc.clip.length);
        subtitles.HideSubtitle();
        audioPlaying = false;
    }


    //Methods related to the InputAction
    private void OnEnable()
    {
        primaryPress.Enable();
    }

    private void OnDisable()
    {
        primaryPress.Disable();
    }    

}
