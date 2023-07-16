using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Handles dialogue audio for the NPC characters
 */
public class CharacterConversation : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject player;
    [SerializeField] private InputAction primaryPress;//
    [SerializeField] private Subtitles subtitles;
    private UserInventory userInventory; //
    private WomanMovement womanMovement;
    private bool canTalk;
    private bool userFirst;
    private Animator animator;
    private AudioSource audioSrc;
    private int counter;
    private int subtitleCounter;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        userInventory = player.GetComponent<UserInventory>();
        audioSrc = GetComponent<AudioSource>();
        counter = 0;
        if (gameObject.tag == "Jill")
           womanMovement =  gameObject.GetComponent<WomanMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (primaryPress.WasPressedThisFrame() && canTalk && !UserDialogue.audioPlaying) //Checks for controller primary button press
        {
            //If statements to check which events are complete and play the appropriate line of dialogue when interacting with an NPC

            //ensures letter event has happened and user hasn't already had this conversation before continuing
            if (userInventory.IsEventComplete("LetterReceived") && userInventory.IsEventComplete("ParentConversation") == false && gameObject.tag == "Remy")
            {
                UserDialogue.audioPlaying = true;                
                ConversationOne();
            }

            if (!userInventory.IsEventComplete("LetterReceived") && gameObject.tag == "Remy")
            {
                UserDialogue.audioPlaying = true;
                MaleSingleLineA();
            }

            if (!userInventory.IsEventComplete("LetterReceived") && userInventory.IsEventComplete("InsuranceCall") && gameObject.tag == "Jill")
            {
                UserDialogue.audioPlaying = true;
                FemaleSingleLineB();
            }
            if (userInventory.IsEventComplete("LetterReceived") && gameObject.tag == "Jill")
            {
                UserDialogue.audioPlaying = true;
                FemaleSingleLineC();
            }

            if (userInventory.IsEventComplete("ParentConversation") && gameObject.tag == "Jill")
            {
                UserDialogue.audioPlaying = true;
                FemaleSingleLineD();
            }


            if (userInventory.IsEventComplete("ParentConversation") && gameObject.tag == "Remy")
            {
                UserDialogue.audioPlaying = true;
                MaleSingleLineB();
            }

            if (userInventory.IsEventComplete("PoliceCall") && gameObject.tag == "Remy")
            {
                UserDialogue.audioPlaying = true;
                ConversationTwo();
            }
        }

        
    }

    /**
     * Trigger checks user has entered collision box, sets bool value to enable interaction.
     */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canTalk = false;
        }
    }

    /**
     * Calls the next line of dialogue from the conversation array
     */
    public void ConversationOne()
    {       
        if (counter < audioManager.ConversationArrayLength("parentUserDialogue1"))
        {
            userFirst = true;
            StartCoroutine(Conversation("userParentDialogue1", "parentUserDialogue1", "Talking", "ParentConversation"));
        }
    }

    /**
     * Starts second conversation with male character passing relevant parameters to coroutine
     */
    public void ConversationTwo()
    {
        if (counter < audioManager.ConversationArrayLength("maleCinemaDialogue"))
        {
            userFirst = false;
            StartCoroutine(Conversation("maleCinemaDialogue", "userCinemaDialogue", "SittingTalking", "FinalConversation"));
        }
        
    }

    /**
     * Coroutine calls dialogue line from one array and then from another dependant on who is speaking first. Calls
     * method from audio manager to play dialogue, play animations, and call subtitle line.
     * @param dialogue array item to be played first
     * @param dialogue array item to be played second
     * @param animation to be played while speaking
     * @param scene event to be updated
     */
    IEnumerator Conversation(string firstDialogue, string secondDialogue, string animation, string sceneEvent)
    {
        while (counter < audioManager.ConversationArrayLength(firstDialogue))
        {
            if(!userFirst)
                animator.SetBool(animation, true);
            audioManager.NpcConversation(firstDialogue, counter, gameObject);
            subtitles.DisplaySubtitleArray(firstDialogue,subtitleCounter);
            subtitleCounter++;
            yield return new WaitForSeconds(audioSrc.clip.length);
            if(!userFirst)
                animator.SetBool(animation, false);
            if(userFirst)
                animator.SetBool(animation, true);
            audioManager.NpcConversation(secondDialogue, counter, gameObject);
            subtitles.DisplaySubtitleArray(firstDialogue,subtitleCounter);
            subtitleCounter++;
            yield return new WaitForSeconds(audioSrc.clip.length);
            subtitles.HideSubtitle();
            if(userFirst)
                animator.SetBool(animation, false);
            
            counter++;
        }
        if (counter == audioManager.ConversationArrayLength("parentUserDialogue1"))
        {
            GetComponent<ManMovement>().SendMessage("StartMovement");
        }
        counter = 0;
        subtitleCounter = 0;
        UserDialogue.audioPlaying = false;        
        userInventory.SendMessage("UpdateProgress", sceneEvent); //update log of events      
        
    }

    //Methods to call coroutine to play various single lines of dialogue
    private void FemaleSingleLineA()
    {
        StartCoroutine(SingleLineDialogue("WomanDialogue1", "Talking"));
    }
    private void FemaleSingleLineB()
    {
        StartCoroutine(SingleLineDialogue("f-dialogue2", "SittingTalking")); //I think you have a letter
    }

    private void FemaleSingleLineC()
    {
        StartCoroutine(SingleLineDialogue("f-dialogue3", "SittingTalking")); //after letter picked up
        
    }

    private void FemaleSingleLineD()
    {
        StartCoroutine(SingleLineDialogue("f-dialogue4", "Talking")); //after letter picked up and Remy convo
    }

    private void MaleSingleLineA()
    {
        StartCoroutine(SingleLineDialogue("m-dialogue1", "Talking")); //Did I hear a letter
    }
    private void MaleSingleLineB()
    {
        StartCoroutine(SingleLineDialogue("m-dialogue2", "SittingTalking")); //After Remy Convo
    }

    /**
     * Coroutine to play a single line of dialogue.
     * @param Dialogue line to play
     * @param animation to play with dialogue line
     */
    IEnumerator SingleLineDialogue(string DialogueLine, string animation)
    {
        animator.SetBool(animation, true);
        audioManager.PlayOnObject(DialogueLine, gameObject);
        subtitles.DisplaySubtitle(DialogueLine);
        yield return new WaitForSeconds(audioSrc.clip.length);
        subtitles.HideSubtitle();
        animator.SetBool(animation, false);
        UserDialogue.audioPlaying = false;
        if (gameObject.tag == "Jill" && !userInventory.IsEventComplete("InsuranceCall"))
            womanMovement.BeginMovement();
        
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
