using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private Toggle subsToggle;
    [SerializeField] private Toggle snapTurnToggle;
    [SerializeField] private Toggle conTurnToggle;
    [SerializeField] private GameObject xrRig;
    private ActionBasedSnapTurnProvider playerSnapTurn;
    private ActionBasedContinuousTurnProvider playerConTurn;
    private bool activateSubs = false;
    private bool activeSnapTurn = false;
    private bool activeConTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (xrRig != null)// check to ensure this is not null
        {
            playerSnapTurn = xrRig.GetComponent<ActionBasedSnapTurnProvider>();
            playerConTurn = xrRig.GetComponent<ActionBasedContinuousTurnProvider>();
            SetToggleStates();
        }

    }

    /**
     * Sets the playerprefs int to either 1 to denote true, or 0 to denote false
     * This can be used to activate subtitles in susequent scenes
     */
    public void ToggleSubtitles()
    {
        activateSubs = !activateSubs;
        if (activateSubs)
        {
            PlayerPrefs.SetInt("Subtitles", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Subtitles", 0);
        }
    }

    /**
     * Sets the state of the toggle switches using the player prefs value for either the 
     * subtitles or turning settings.
     */
    private void SetToggleStates()
    {
        if (PlayerPrefs.GetInt("Subtitles") == 1)//subtitles on
        {
            subsToggle.isOn = true;
        } else
        {
            subsToggle.isOn = false;
        }
        
        if (PlayerPrefs.GetInt("SnapTurn") == 1) //snap turn obn
        {
            snapTurnToggle.isOn = true;
            conTurnToggle.isOn = false;
        }
        else
        {
            conTurnToggle.isOn = true;
            snapTurnToggle.isOn = false;
        }
        
    }

    /**
     * Method to toggle turn type, ensures that only one type of turning can be activated.
     * @param integer value passed from the toggle gameObject, each toggle passes a different value.
     */
    public void ToggleTurnType(int activeTogggle)
    {
        
        int switchCase;
        if (activeSnapTurn)
            switchCase = 2;
        else if (activeConTurn)
            switchCase = 1;
        else switchCase = activeTogggle;

        switch (switchCase)
        {
            case 1:
                playerSnapTurn.enabled = true;
                playerConTurn.enabled = false;
                conTurnToggle.isOn = false;
                snapTurnToggle.isOn = true;
                activeSnapTurn = true;
                activeConTurn = false;
                PlayerPrefs.SetInt("SnapTurn", 1);
                
                
                break;
            case 2:
                playerSnapTurn.enabled = false;
                playerConTurn.enabled = true;
                conTurnToggle.isOn = true;
                snapTurnToggle.isOn = false;
                activeConTurn = true;
                activeSnapTurn = false;
                PlayerPrefs.SetInt("SnapTurn", 0);
                
                break;
        }

    }

}
