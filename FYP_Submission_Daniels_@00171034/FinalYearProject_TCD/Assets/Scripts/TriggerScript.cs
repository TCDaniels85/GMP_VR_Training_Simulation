using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Script to trigger events within the scene
 */
public class TriggerScript : MonoBehaviour
{
    public GameObject objectToTrigger;
    private bool phoneTriggerComplete;
    private bool dialogueTrigger1Complete;
    private bool letterTriggerComplete;
    private bool phoneEventHintComplete;
    
    // Start is called before the first frame update
    
    /**
     * If statements used within the the on trigger enter method to check the object entering the box collider
     * and use the send message function to call the method on the objectToTrigger variable.
     */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && gameObject.tag == "LetterTrigger" && !letterTriggerComplete)
        {
            objectToTrigger.SendMessage("SendLetter");
            letterTriggerComplete = false;
        }

        if(other.gameObject.tag == "Player" && gameObject.tag == "PhoneTrigger" && !phoneTriggerComplete)
        {
            objectToTrigger.SendMessage("PhoneRing");
            phoneTriggerComplete = true;
        }

        if (other.gameObject.tag == "Player" && gameObject.tag == "TalkTriggerUpstairs" && !dialogueTrigger1Complete)
        {            
            objectToTrigger.SendMessage("FemaleSingleLineA"); 
            dialogueTrigger1Complete = true;
        }

        if(other.gameObject.tag == "Player" && gameObject.tag == "Hint" )
        {
            if(objectToTrigger != null) //Checks this object is not null as these are destroyed after ShowHint is called
            {
                ShowHint();
            }                        
        }

        if((other.gameObject.tag=="LeftHand" || other.gameObject.tag == "RightHand") && gameObject.name == "Abusive Letter")
        {
            objectToTrigger.GetComponent<UserInventory>().SendMessage("UpdateProgress", "LetterReceived");
            
        }
        if ((other.gameObject.tag == "LeftHand" || other.gameObject.tag == "RightHand") && gameObject.name == "Tablet")
        {
            objectToTrigger.GetComponent<UserInventory>().SendMessage("UpdateProgress", "SocialMedia");
            ;
        }
        if (other.gameObject.tag == "Player" && gameObject.tag == "StartUI")
        {
            RemoveUI();
        }

        if(other.gameObject.tag == "Player" && gameObject.tag == "DialogueHint" && !phoneEventHintComplete)
        {
            bool phoneVentComplete = objectToTrigger.GetComponent<UserInventory>().IsEventComplete("InsuranceCall");
            if (!phoneVentComplete && !UserDialogue.audioPlaying)
            {
                objectToTrigger.GetComponent<UserDialogue>().PlayDialogueHint("InternalDialogue1");
            }            
            phoneEventHintComplete = true;
        }
    }

    /**
     * Shows the hint UI by enabling the gameObject, destroy this after 7 seconds.
     */

    private void ShowHint()
    {
        objectToTrigger.SetActive(true);
        Destroy(objectToTrigger,7.0f);
    }
    
    /**
     * Deactivates UI object
     */
    private void RemoveUI()
    {
        objectToTrigger.SetActive(false);
    }

   

}
