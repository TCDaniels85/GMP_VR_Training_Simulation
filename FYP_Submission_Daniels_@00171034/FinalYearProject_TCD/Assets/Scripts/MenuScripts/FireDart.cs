using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Basic script to launch a dart from the dart gun
 */
public class FireDart : MonoBehaviour
{    
    
    [SerializeField] private Rigidbody dartPrefab;
    [SerializeField] private GameObject projectileStart;
    private bool canPress;
    private AudioSource audioSrc;
    [SerializeField] InputAction primaryPress;

    private float launchPower = 10.0f;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }


    private void Update()
    {
        //Checks dart gun is in hand and button is pressed
        if (primaryPress.WasPressedThisFrame() && canPress)
        {
            Fire();            
        }
    }


    /**
     * Checks the dartgun is in the user's hand
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {

            canPress = true;
        }
    }

    /**
     * Stops the user from firing when the dart gun is dropped
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            canPress = false;
        }
    }

    /**
     * Fires dart from gun
     */
    public void Fire()
    {
        Rigidbody newDart = Instantiate(dartPrefab, projectileStart.transform.position, projectileStart.transform.rotation) as Rigidbody; //Instantiate dart prefab at correct rotation and position       
        newDart.name = "dart";
        newDart.velocity = transform.forward * launchPower;
        audioSrc.Play();
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
