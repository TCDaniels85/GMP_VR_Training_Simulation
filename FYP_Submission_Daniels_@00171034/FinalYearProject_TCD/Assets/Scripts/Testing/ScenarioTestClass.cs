using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class used in testing
 */
public class ScenarioTestClass : MonoBehaviour
{
    public GameObject player;
    public GameObject remy;
    private ManMovement remyMovementScript;
    private UserInventory userInventory;
    public bool insuranceCallTest;
    public bool letterReceivedTest;
    public bool parentConversationTest;
    public bool policeCallTest;
    public bool remyFirstMovement;
    public bool socialMediaTest;
    public bool reluctanceConversationTest;
    public bool endScene;
    public bool textArrayTest;

    // Start is called before the first frame update
    void Start()
    {
        remyMovementScript = remy.GetComponent<ManMovement>();
        userInventory = player.GetComponent<UserInventory>();
        insuranceCallTest = false;
        letterReceivedTest = false;
        parentConversationTest = false;
        policeCallTest = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Testing inventory updates
        if (insuranceCallTest)
        {
            userInventory.UpdateProgress("InsuranceCall");
            insuranceCallTest = false;
        }
        if (letterReceivedTest)
        {
            userInventory.UpdateProgress("LetterReceived");
            letterReceivedTest = false;
        }
        if (parentConversationTest)
        {
            userInventory.UpdateProgress("ParentConversation");
            parentConversationTest = false;
        }
        if (policeCallTest)
        {
            userInventory.UpdateProgress("PoliceCall");
            policeCallTest = false;
        }
        if (socialMediaTest)
        {
            userInventory.UpdateProgress("SocialMedia");
            socialMediaTest = false;
        }
        if (reluctanceConversationTest)
        {
            userInventory.UpdateProgress("FinalConversation");
            reluctanceConversationTest = false;
        }
        if (remyFirstMovement)
        {
            //remyMovementScript
            remyMovementScript.StartMovement(); //make protected after testing
        }

        //Test end of scene, checks user inventory events are recorded properly
        if (endScene)
        {
            //userInventory.EndScene(); //make private after testing
        }

       
    }


}
