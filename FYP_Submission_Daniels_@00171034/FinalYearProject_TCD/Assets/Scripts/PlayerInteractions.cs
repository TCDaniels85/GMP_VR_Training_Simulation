using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/**
 * Script to switch interactable object collider to a trigger to prevent the collider interfering
 * with xrRig colliders
 * Script no longer used, the use of layers was employed to prevent interactable and player colisions.
 */
public class PlayerInteractions : MonoBehaviour
{
    private static bool isGrabbed;   
    private static bool isGrabDisabled;

    private XRRayInteractor rayGrab;
    private XRDirectInteractor xrGrab;

    
    void Start()
    {
        if (this.name.Contains("Ray")) //gets reference to interactors dependant on name
        {
            
            rayGrab = GetComponent<XRRayInteractor>();
            rayGrab.selectEntered.AddListener(Grabbing);
            rayGrab.selectExited.AddListener(Dropping);
        }
        else
        {
            xrGrab = GetComponent<XRDirectInteractor>();
            xrGrab.selectEntered.AddListener(Grabbing);
            xrGrab.selectExited.AddListener(Dropping);
        }
        
    }

    

    /**
     * Sets isGrabbed to true whne object being grabbed
     */
    public void Grabbing(SelectEnterEventArgs args)
    {
        
        isGrabbed = true;
        
    }

    /**
     * Sets isGrabbed to false when let go
     */
    public void Dropping(SelectExitEventArgs args)
    {                
        isGrabbed = false;
    }

    
    /**
     * Sets object entering hand trigger collider to be a trigger to avoid interfearing with other colliders on the player object
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable" && isGrabbed && !isGrabDisabled)        
        {
            other.GetComponent<Collider>().isTrigger = true;
            isGrabDisabled = true;

        }
    }

    /**
     * Sets collider back to original state
     */
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Interactable" && !isGrabbed)
        //if (other.gameObject.tag == "Interactable")
        {

            other.GetComponent<Collider>().isTrigger = false;
            isGrabDisabled = false;
        }

    }

    /**
     * Sets hand collider to trigger dependant on whether the controller is grabbing an object
     * Not used at the moment - Delete if not used
     */
    private void HandColliderTrigger()
    {
        if (isGrabbed)
        {
            GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }
        
}





