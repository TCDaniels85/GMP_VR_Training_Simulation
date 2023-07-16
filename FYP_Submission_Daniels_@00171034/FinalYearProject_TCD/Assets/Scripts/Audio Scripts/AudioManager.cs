using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using System;

/**
 * Audio manager class that handles sounds from the ProjectAudio and Audio3D classes
 */
public class AudioManager : MonoBehaviour
{
    public ProjectAudio[] audioClips;  //Array of 2d sounds
    
    
    public Audio3D[] audioClips3D;      //Array of 3d sounds

    private Dictionary<String, Audio3D[]> conversationsDictionary;
    public Audio3D[] parentUserDialogue1;
    public Audio3D[] userParentDialogue1;
    public Audio3D[] policeUserDialogue1;
    public Audio3D[] userPoliceDialogue1;
    public Audio3D[] bankUserPhoneDialogue;
    public Audio3D[] userBankPhoneDialogue;
    public Audio3D[] maleCinemaDialogue;
    public Audio3D[] userCinemaDialogue;
    public Audio3D[] femaleConvo1;
    public Audio3D[] maleConvo2;
    public Audio3D[] bathroomNarration; 

    // Start is called before the first frame update
    void Awake()
    {
        foreach(ProjectAudio sound in audioClips)
        {
            //Adds audio source for each clip in audioClips array, populates audiosource valus according to what has been set on each array item
            sound.setAudioSource(gameObject.AddComponent<AudioSource>());
            AudioSource track = sound.GetAudioSource();
            track.clip = sound.audioClip;
            track.volume = sound.Volume;            
            track.loop = sound.loop;            
        }

        conversationsDictionary = new Dictionary<String, Audio3D[]>(); //Create a dictionary of conversation arrays so these can be accessed through another class
        conversationsDictionary.Add("parentUserDialogue1", parentUserDialogue1);
        conversationsDictionary.Add("userParentDialogue1", userParentDialogue1);
        conversationsDictionary.Add("policeUserDialogue1", policeUserDialogue1);
        conversationsDictionary.Add("userPoliceDialogue1", userPoliceDialogue1);
        conversationsDictionary.Add("bankUserPhoneDialogue", bankUserPhoneDialogue);
        conversationsDictionary.Add("userBankPhoneDialogue", userBankPhoneDialogue);
        conversationsDictionary.Add("maleCinemaDialogue", maleCinemaDialogue);
        conversationsDictionary.Add("userCinemaDialogue", userCinemaDialogue);
        conversationsDictionary.Add("bathroomNarration", bathroomNarration);

    }

    /**
     * Plays sounds in the Audio manager game object, used for 2d sounds
     * @param name of the clip to play
     */
    public void PlaySound(string clipName)
    {
        
        ProjectAudio sound = Array.Find(audioClips, audioClips => audioClips.name == clipName);  //Lambda to find clip name in array
        if(sound == null)
        {
            Debug.Log("Sound missing");
            return;
        }
        sound.GetAudioSource().Play();
    }

    /**
     * Creates an audiosource on a the required game object which can be used to play the required track.
     * Used for 3d sounds
     * @param name of the clip to play
     * @param game object containing audio source clip is to be played on
     */
    public void PlayOnObject(String clipName, GameObject gameObject)
    {
        AudioSource newSound;
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            newSound = gameObject.AddComponent<AudioSource>();  //adds audioSource to game object
        }
        else
        {
            newSound = gameObject.GetComponent<AudioSource>();  //finds audioSource on game object
        }
        
        Audio3D sound = Array.Find(audioClips3D, audioClips3D => audioClips3D.clipName == clipName);
        if (sound == null)
        {
            Debug.Log("Sound missing");
            return;
        }
        //Assign all values from array object to audio source created
        newSound.clip = sound.audioClip;
        newSound.volume = sound.Volume;
        newSound.spatialBlend = sound.spatialBland;
        newSound.loop = sound.loop;
        newSound.minDistance = sound.minDistance;
        newSound.maxDistance = sound.maxDistance;
        newSound.Play();
    }

    /**
     * Stops audio playing on an object
     * @param game object on which to stop audio playing
     */
    public void StopAudio(GameObject gameObject)
    {
        if(gameObject.GetComponent<AudioSource>() != null)
            gameObject.GetComponent<AudioSource>().Stop();
    }

    /**
     * Adds audio source component on the NPC character amd plays audio track from desired array (retrieved from dictionary)
     * dependant on index provided
     * @param name of conversation array
     * @param index of array to be played
     * @param game object containing audio source clip is to be played on
     */
    public void NpcConversation(String arrayName, int index, GameObject gameObject)
    {
        AudioSource newSound;
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            newSound = gameObject.AddComponent<AudioSource>();  //adds audioSource to game object
        }
        else
        {
            newSound = gameObject.GetComponent<AudioSource>();  //finds audioSource on game object
        }
        Audio3D[] conversation =  conversationsDictionary[arrayName]; //find the conversation array to be played
        Audio3D sound = conversation[index]; //gets clip from array
        if (sound == null)
        {
            Debug.Log("Sound missing");
            return;
        }
        newSound.clip = sound.audioClip;
        newSound.volume = sound.Volume;
        newSound.spatialBlend = sound.spatialBland;
        newSound.loop = sound.loop;
        newSound.minDistance = sound.minDistance;
        newSound.maxDistance = sound.maxDistance;
        newSound.Play();
    }

    /**
     * Retruns the length of an array in the conversationDictionary.
     * @param name of array 
     */
    public int ConversationArrayLength(String arrayName)
    {
        Audio3D[] conversation = conversationsDictionary[arrayName];
        if(conversation == null)
        {
            Debug.LogError("This array cannot be found");
            return 0;
        }
        return conversation.Length;
    }

    
}
