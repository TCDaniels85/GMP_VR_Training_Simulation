using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/**
 * Script to set transform point on grabbable object dependant on which hand is interacting.
 */
public class DominantHand : MonoBehaviour
{
    //references to the left and right controllers, and relevent object attach points
    public GameObject rightDirectInteractor;
    public GameObject leftDirectInteractor;
    public GameObject rightRayInteractor;
    public GameObject leftRayInteractor;
    public Transform rightAttachPoint;
    public Transform leftAttachPoint;
    XRGrabInteractable xrGrab;
    
    // Start is called before the first frame update
    void Start()
    {
        xrGrab = GetComponent<XRGrabInteractable>();
    }

    /**
     * Selects the attach point for the xrGrabInteractable according to which hand is interacting
     */
    public void ChooseHandedness()
    {
        
        if (xrGrab.selectingInteractor.name == rightDirectInteractor.name || xrGrab.selectingInteractor.name == rightRayInteractor.name)        
        {            
            xrGrab.attachTransform = rightAttachPoint;
            
        }
        
        if (xrGrab.selectingInteractor.name == leftDirectInteractor.name || xrGrab.selectingInteractor.name == leftRayInteractor.name)
        {
            xrGrab.attachTransform = leftAttachPoint;
        }
        
    }
}
