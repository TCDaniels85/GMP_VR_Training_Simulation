using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Script to display a basic results screen
 */
public class ResultsUiScript : MonoBehaviour
{
    public Texture2D[] checkMark;
    [SerializeField] private RawImage insuranceCall;
    [SerializeField] private RawImage letter;
    [SerializeField] private RawImage supportHouseMate;
    [SerializeField] private RawImage supportPolice;
    [SerializeField] private RawImage socialMedia;
    [SerializeField] private RawImage reluctanceToLeave;
    private Dictionary<RawImage, int> results;



    // Start is called before the first frame update
    void Start()
    {
        //Creates a dictionary from the playerPrefs
        results = new Dictionary<RawImage, int>();
        results.Add(insuranceCall, PlayerPrefs.GetInt("InsuranceCall"));
        results.Add(letter, PlayerPrefs.GetInt("LetterReceived"));
        results.Add(supportHouseMate, PlayerPrefs.GetInt("ParentConversation"));
        results.Add(supportPolice, PlayerPrefs.GetInt("PoliceCall"));
        results.Add(socialMedia, PlayerPrefs.GetInt("SocialMedia"));
        results.Add(reluctanceToLeave, PlayerPrefs.GetInt("FinalConversation"));

        SetChecks(results); //Loop through dictionary to set check marks
        
    }

    /**
     * Loops through items in the results dictionary to set the relevant texture in the UI 
     * (tick for complete, cross for missed) according to the value.
     */
    private void SetChecks(Dictionary<RawImage, int> results)
    {

        foreach (KeyValuePair<RawImage, int> pair in results)
        {
            
            if (pair.Value == 0)
            {
                pair.Key.texture = checkMark[0];
            }
            else
            {
                pair.Key.texture = checkMark[1];
            }
        }
    }

    /**
     * Continue to next scene
     */
    public void Continue()
    {
        PlayerPrefs.DeleteAll();
        SceneLoader.LoadMyScene(0);
    }
}
