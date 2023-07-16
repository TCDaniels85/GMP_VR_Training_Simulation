using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Script to handle the conversation dialogue with the phone
 */
public class PhoneConversationBank : MonoBehaviour
{
    public Texture2D[] screen;
    public Renderer phoneScreen;
    public bool isRinging;
    
    private bool isPlaying;
    
    [SerializeField] private AudioManager audioManager;    
    [SerializeField] private GameObject player;
    [SerializeField] private InputAction primaryPress;
    [SerializeField] private Subtitles subtitles;

    private AudioSource audioSrc;    
    private bool canDial;
    private bool canAnswer;
    private int counter;
    private int subtitleCounter;
    private UserInventory userInventory;
    
    // Start is called before the first frame update
    void Start()
    {
        phoneScreen.material.mainTexture = screen[0]; //Set phone screen background
        audioSrc = GetComponent<AudioSource>();
        userInventory = player.GetComponent<UserInventory>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (primaryPress.WasPressedThisFrame() && canDial)
        {
            //Ensures parent conversation event is complete and that this phonecall has not been made before continuing
            if(userInventory.IsEventComplete("ParentConversation") && userInventory.IsEventComplete("PoliceCall") == false)
            {
                //player.SendMessage("SetAudioPlaying"); //Sets bool to true on user script to prevent dilogue object setting to null
                UserDialogue.audioPlaying = true; //Sets bool to true on user script to prevent dilogue object setting to null
                PoliceConversation();                
                player.SendMessage("UpdateProgress", "PoliceCall"); // update log of events
            }            
        }

        if(primaryPress.WasPressedThisFrame() && canAnswer)
        {
            if(userInventory.IsEventComplete("InsuranceCall") == false) //Ensures this event can't happen twice
            {
                //player.SendMessage("SetAudioPlaying"); //Sets bool to true on user script to prevent dilogue object setting to null
                UserDialogue.audioPlaying = true; //Sets bool to true on user script to prevent dilogue object setting to null
                canAnswer = false;
                isRinging = false;                
                InsurancePhoneCall();
                player.SendMessage("UpdateProgress", "InsuranceCall"); //update log of events  
            }
        }
        
    }

    /**
     * Checks the phone object is in hand and dependant on whether the phone is ringing or playing audio allows the 
     * user to answer or make a phone call
     */
    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand") && isRinging && !isPlaying)
        {            
            canAnswer = true;            
        }
        if ((other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand") && !isRinging)
        {
            canDial = true;
        }
    }

    /**
     * Disable user ability to make phone call when phone is out of trigger zone (user hand)
     */
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand"))
        {
            canDial = false;
        }
    }

    /**
     * method handling audio and textures when the phone is ringing
     */
    private void PhoneRing()
    {
            audioManager.PlayOnObject("RingTone", gameObject);
            phoneScreen.material.mainTexture = screen[1]; //Change phone screen to dialing texture
            isRinging = true;       
    }    

    /**
     * Handles the phone conversation with the police. Begins the coroutine on for dialogue passing variables
     * to denote the dialogue array which is top be used and the method in the user dialogue script to be called once the 
     * line of dialogue has finished.
     * When the counter reaches the end of the dialogue, the relvant message on the user script is called to end that dialogue method.
     */
    private void PoliceConversation()
    {        
        if (counter < audioManager.ConversationArrayLength("policeUserDialogue1"))
        {
            phoneScreen.material.mainTexture = screen[2];
            StartCoroutine(NextLine("policeUserDialogue1", "userPoliceDialogue1"));
        }          
    }

    /**
     * Handles the phone conversation with insurance agent, Begins the coroutine on for dialogue passing variables.
     */
    private void InsurancePhoneCall()
    {
        string userDialogueArray = "userBankPhoneDialogue"; 
        string phoneDialogueArray = "bankUserPhoneDialogue";
        phoneScreen.material.mainTexture = screen[2];
        if (counter < audioManager.ConversationArrayLength("bankUserPhoneDialogue"))
        {            
            isPlaying = true;
            StartCoroutine(NextLine(phoneDialogueArray, userDialogueArray));                        
        } 
    }

    /**
     * Coroutine handling next line of dialogue
     * Retrieves appropriate dialogue array from audio manager and plays each item relating to the counter
     * @param dialogue array relating to phone agent
     * @param dialogue array relating to the user
     */
    IEnumerator NextLine(string phoneDialogue, string userDialogue)
    {
        while (counter < audioManager.ConversationArrayLength(phoneDialogue))
        {
            audioManager.NpcConversation(phoneDialogue, counter, gameObject);
            subtitles.DisplaySubtitleArray(phoneDialogue, subtitleCounter);
            subtitleCounter++;
            yield return new WaitForSeconds(audioSrc.clip.length);
            audioManager.NpcConversation(userDialogue, counter, gameObject);
            subtitles.DisplaySubtitleArray(phoneDialogue, subtitleCounter);
            subtitleCounter++;
            yield return new WaitForSeconds(audioSrc.clip.length);
            subtitles.HideSubtitle();
            counter++;
        }
        counter = 0;
        subtitleCounter = 0;
        phoneScreen.material.mainTexture = screen[0];
        isPlaying = false;
        UserDialogue.audioPlaying = false;
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
