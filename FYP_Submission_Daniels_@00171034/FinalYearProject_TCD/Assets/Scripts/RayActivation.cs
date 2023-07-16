using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


/**
 * Handles the activation and deactivation of the ray interactors 
 */
public class RayActivation : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor directInt;
    private XRRayInteractor rayInt;
    public bool isActivated;

    [SerializeField] private InputAction primaryPress; //Enable binding from inspector



    // Start is called before the first frame update
    void Start()
    {
        primaryPress.performed += _ => EnableRay(); //Lambda that calls designated method following button press
        rayInt = GetComponent<XRRayInteractor>();
        rayInt.enabled = isActivated;

    }   

    private void OnEnable()
    {
        primaryPress.Enable();
    }

    private void OnDisable()
    {
        primaryPress.Disable();
    }

    /**
     * Toggles the ray interactor 
     */
    public void EnableRay()
    {
        if(rayInt.enabled == true){
            rayInt.enabled = false;
            directInt.enabled = true;
        } else if(rayInt.enabled == false)
        {
            rayInt.enabled = true;
            directInt.enabled = false;
        }        
    }
    //Disables rays
    public void DisableRay()
    {
        rayInt.enabled = false;
    }
}
