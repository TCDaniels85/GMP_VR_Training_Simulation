using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class to record the events that have occured in the scene.
 */
public class UserInventory : MonoBehaviour
{
    
    private Dictionary<string, bool> sceneOneEvents; //A dictionary to contain events from the first seen, these can be set to true once they have happened
    private int endSceneCounter;
    private float timer;
    private bool timerActive;
    

    void Start()
    {
        //Scene events, these events enable the control of other events in the scene using conditionals which check the 
        //status of the relevant event.
        sceneOneEvents = new Dictionary<string, bool>();
        sceneOneEvents.Add("InsuranceCall", false);
        sceneOneEvents.Add("LetterReceived", false);
        sceneOneEvents.Add("ParentConversation", false);
        sceneOneEvents.Add("PoliceCall", false);
        sceneOneEvents.Add("SocialMedia", false);
        sceneOneEvents.Add("FinalConversation", false);
        
    }

    private void Update()
    {
        
        if (timerActive)
        {
            timer += Time.deltaTime;            
            if(timer > 180)
            {
                InternalHint();
            }
        }
        
    }

    /**
     * Method to save the key value pair from the sceneOneEvents dictionary as player prefs so they can be accessed in the results scene.
     */
    private void SaveEvents()
    {
        //Loops through dictionary items, uses the key value to name the playerPref and stores the bool value as an int.
        foreach (KeyValuePair<string, bool> pair in sceneOneEvents)
        {
            int boolValue;
            if(pair.Value == true)
            {
                boolValue = 1;
            } else
            {
                boolValue = 0;
            }
            PlayerPrefs.SetInt(pair.Key, boolValue);            
        }
    }

    
    /**
     * Updates the user event log for the scene, updates the relevant event to true once
     * it has occurred.
     * @param name of the event to update
     */
    public void UpdateProgress(string eventName)
    {
        if (eventName == "LetterReceived" && !IsEventComplete("LetterReceived")) //Starts timer for hint only if letterReceived event is not complete
            StartTimer();

        if (!sceneOneEvents[eventName])
        {
            sceneOneEvents[eventName] = true;
            endSceneCounter++;
        }
        if(endSceneCounter == sceneOneEvents.Count || eventName == "FinalConversation")
        {
            StartCoroutine(EndScene());
        }
        
        if (eventName == "ParentConversation")
            StopTimer();

    }

    /**
     * Returns whether or not the event has occured
     * @param name of the event to check
     */
    public bool IsEventComplete(string eventName)
    {
        return sceneOneEvents[eventName];
    }

    /**
     * Ends the scene, calls the SaveEvents method to record what events have been triggered and
     * loads the results scene Includes a 3 second delay to scene ending.
     */
    IEnumerator EndScene()
    {
        SaveEvents();
        yield return new WaitForSeconds(3);
        SceneLoader.LoadMyScene(3);
    }

    

    /**
     * calls the PlayDialogueHint methog on the user dialogue script and stopes the timer.
     */
    private void InternalHint()
    {
        gameObject.SendMessage("PlayDialogueHint", "InternalDialogue2");
        StopTimer();
    }

    private void StartTimer()
    {
        timer = 0;
        timerActive = true;
    }

    private void StopTimer()
    {
        timerActive = false;
    }

    
}



