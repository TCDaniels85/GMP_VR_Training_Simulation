using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ClickableTutorialScript : MonoBehaviour
{
    private bool canPress;
    [SerializeField] InputAction primaryPress;
    private Material originalMat;
    private MeshRenderer meshRender;
    public Material newMat;
    private bool isChanged;

    /**
     * Short script to change the material on a cube in the tutorial after a user has pressed the
     * primary button
     */
    private void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        originalMat = meshRender.material; //stores cube original material in a variable
    }

    private void Update()
    {
        //Checks cube is in hand and button is pressed
        if (primaryPress.WasPressedThisFrame() && canPress)
        {            
            ChangeColour();            
        }
    }

    /**
     * Method to change the colour of the cube. Changes the material on the cube based on a bool 
     * value which is set to the opposing value evertime the method is called
     */
    public void ChangeColour()
    {
       if(originalMat != null && newMat != null){
            if (isChanged)
            {
                meshRender.material = originalMat;
            } else
            {
                meshRender.material = newMat;
            }
            isChanged = !isChanged;
        }
    }

    /**
     * Checks the cube is in the user's hand
     */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            
            canPress = true;
        }
    }

    /**
     * Stops the user from being able to change the colour when the cube is dropped
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            canPress = false;
        }
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
