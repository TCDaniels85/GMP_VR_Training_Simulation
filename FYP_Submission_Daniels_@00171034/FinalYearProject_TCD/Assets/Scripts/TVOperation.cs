using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Script to operate the television within the screen, switches 2d textures on the screen to simulate
 * viewing different streaming/tv providers
 */
public class TVOperation : MonoBehaviour
{
    [SerializeField] private Renderer tvScreenRenderer;
    [SerializeField] private Texture2D[] tvScreen;
    [SerializeField] private InputAction primaryPress;
    private AudioSource audioSrc;
    private bool canOperateTv = false;
    private int tvState;
    // Start is called before the first frame update
    void Start()
    {
        tvState = 0;
        tvScreenRenderer.material.mainTexture = tvScreen[0];
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (primaryPress.WasPressedThisFrame() && canOperateTv) 
        {            
            OperateTv();            
        }
    }

    /**
     * Cycles through an array of 2d textures each operation
     */
    private void OperateTv()
    {        
        tvState++;        
        if (tvState == tvScreen.Length) //reset counter to beginning of array when it reaches the end
        {
            tvState = 0;
        }
        tvScreenRenderer.material.mainTexture = tvScreen[tvState];
        audioSrc.Play();
    }

    /**
     * Sets a bool value to ensure remote can only be operated while in hand
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LeftHand" || other.gameObject.tag == "RightHand")
        {
            canOperateTv = true;
        }
    }
    /**
     * Sets a bool value to ensure remote can only be operated while in hand
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LeftHand" || other.gameObject.tag == "RightHand")
        {
            canOperateTv = false;
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
