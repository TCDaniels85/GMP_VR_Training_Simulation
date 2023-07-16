using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialInteractions : MonoBehaviour
{
    [SerializeField] private GameObject tutorialTable;
    [SerializeField] private GameObject grabTutorialObject;
    [SerializeField] private GameObject activationTutorialObject;
    [SerializeField] private GameObject xrRig;
    [SerializeField] private GameObject tutorialItemGroup;
    private ActionBasedContinuousMoveProvider playerMovement;
    private ActionBasedSnapTurnProvider playerTurn;
    private ActionBasedContinuousTurnProvider playerContinuousTurn;
    private Vector3 grabbingTablePosition;
    private Vector3 grabbingCubePosition;
    private Vector3 rayTablePosition;
    private Vector3 rayCubePosition;
    private Vector3 activationCubePosition;
    private Vector3 playerStartPosition;
    private Quaternion playerStartRotation;

    // Start is called before the first frame update
    void Start()
    {
        grabbingTablePosition = new Vector3(-0.007f, 0f, 4.163f);
        grabbingCubePosition = new Vector3(0.133f, 1.184f, 3.824f);
        rayCubePosition = new Vector3(0.133f, 1.184f, 7.23f);
        rayTablePosition = new Vector3(-0.007f, 0f, 7.41f);
        activationCubePosition = new Vector3(0.133f, 1.184f, 3.824f);
        playerStartPosition = new Vector3(0f,0f,3.292f);
        playerStartRotation = xrRig.transform.rotation;

        if (xrRig != null)// check to ensure this is not null
        {
            playerMovement = xrRig.GetComponent<ActionBasedContinuousMoveProvider>();
            playerTurn = xrRig.GetComponent<ActionBasedSnapTurnProvider>();
            playerContinuousTurn = xrRig.GetComponent<ActionBasedContinuousTurnProvider>();
        }

        
    }    

    /**
     * Method to display the tutorial table object and the relevant interactable object in front of the player for
     * this section of the tutorial. Sets the objects to active to make them visible in the scene then sets the position
     * of the table and interactable object. 
     */
    public void DisplayGrabbingTable()
    {        
        tutorialTable.SetActive(true);
        tutorialTable.transform.position = grabbingTablePosition;
        grabTutorialObject.SetActive(true);
        grabTutorialObject.transform.position = grabbingCubePosition;
    }

    /**
     * Method to display the tutorial table object and the relevant interactable object in front of the player for
     * this section of the tutorial. Sets the objects to active to make them visible in the scene then sets the position
     * of the table and interactable object further away from the player to encourage the use of ray interactors.
     */
    public void DisplayRayGrabbingTable()
    {
        //tutorialTable.SetActive(true);
        tutorialTable.transform.position = rayTablePosition;
        //grabTutorialObject.SetActive(true);
        grabTutorialObject.transform.position = rayCubePosition;
    }

    /**
     * Method to display the tutorial table object and the relevant interactable object in front of the player for
     * this section of the tutorial. Sets the objects to active to make them visible in the scene then sets the position
     * of the table and interactable object. 
     */
    public void DisplayActivationGrabbingTable()
    {
        tutorialTable.SetActive(true);
        tutorialTable.transform.position = grabbingTablePosition;
        activationTutorialObject.SetActive(true);
        activationTutorialObject.transform.position = activationCubePosition;
    }

    /**
     * Hides the tutorial items from the user.
     */
    public void HideTutorialTable()
    {
        tutorialTable.SetActive(false);
        grabTutorialObject.SetActive(false);
        activationTutorialObject.SetActive(false);
    }

    /**
     * Allows player movement.
     */
    public void TurnOnMovement()
    {
        playerMovement.enabled = true;
        if (PlayerPrefs.GetInt("SnapTurn") == 1)
            playerTurn.enabled = true;
        else if (PlayerPrefs.GetInt("SnapTurn") == 0)
            playerContinuousTurn.enabled = true;
        else
            playerTurn.enabled = true;




    }

    /**
     * Disables player movement, used at the beginning of the tutorial to focus the user on the menu.
     */
    private void TurnOffMovement()
    {
        playerMovement.enabled = false;
        playerTurn.enabled = false;
        playerContinuousTurn.enabled = false;
    }
    /**
     * Resets the users position and disables movement to enable the tutorial section to begin again (if selected from menu)
     */
    public void ResetPosition()
    {
        TurnOffMovement();
        //Set users position and rotation
        xrRig.transform.position = playerStartPosition;
        xrRig.transform.rotation = playerStartRotation;
    }

    /**
     * Activates the tutorial items within the wider scene, these are deactivated while the 
     * user initially experiences the tutorial to encourage focus.
     */
    public void TurnOnTutorialItems()
    {
        tutorialItemGroup.SetActive(true); //These items are grouped to enable the activation of all items simultaneously.
    }

    public void TurnOffTutorialItems()
    {
        tutorialItemGroup.SetActive(false);
    }

}
